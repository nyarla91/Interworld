using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;

public class Locations : MonoBehaviour
{
    public static Locations instance;
    
    public static Vector2 playerPosition;
    public static int currentLocationNumber;
    public static Location currentLocation;
    [SerializeField] private RectTransform overlayTop;
    [SerializeField] private RectTransform overlayBottom;
    [SerializeField] private int startingLocation;
    [SerializeField] private int startingExit;
    public GameObject dataLocation;
    [SerializeField] private List<GameObject> locations;

    private static float _overlayState;
    private static float overlayState
    {
        get => _overlayState;
        set
        {
            _overlayState = value;
            instance.overlayTop.anchoredPosition = new Vector2(0, -0.5f * value * Screen.height);
            instance.overlayBottom.anchoredPosition = new Vector2(0, 0.5f * value * Screen.height);
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (File.Exists(Application.dataPath + "data.json"))
        {
            print(startingExit);
            Saves.Load();
        }
        else
        {
            print(startingExit);
            LoadLocation(startingLocation, startingExit, SaveType.None);
        }
    }

    public static void LoadLocation(int location, int exitNumber, SaveType saveType)
    {
        instance.StartCoroutine(LoadLocationIE(location, exitNumber, saveType));
    }

    private static IEnumerator LoadLocationIE(int location, int exitNumber, SaveType saveType)
    {
        print(exitNumber);
        // Discard local saves
        if (saveType == SaveType.Global)
            Saves.DiscardLocalSaves();
        currentLocationNumber = location;
        // Hide PlayerUI
        Player.instance.ui.Hide();
        // Blend out
        Player.instance.Reset();
        Controls.freezedControls++;
        for (float i = 0; i < 0.99f; i = Mathf.Lerp(i, 1, 5 * Time.deltaTime) + Time.deltaTime)
        {
            overlayState = i;
            yield return null;
        }
        overlayState = 1;
        yield return new WaitForSeconds(0.2f);
        // Close map
        if (Map.current != null)
            Map.current.ToggleMap(false);
        // Destroy previous location
        if (currentLocation != null)
            Destroy(currentLocation.gameObject);
        // Disable player to prevent it from entering triggers
        Player.instance.gameObject.SetActive(false);
        // Stop combat
        Player.instance.status.InterruptImmune();
        if (CombatArea.combat)
            CombatArea.current.InterruptCombat();
        // Create new location
        currentLocation = Instantiate(instance.locations[location], Vector3.zero, Quaternion.identity).GetComponent<Location>();
        if (currentLocation.exits.Count < exitNumber + 1)
            throw new Exception($"There is no exit {exitNumber} in this location");
        AstarData.active.Scan(AstarData.active.graphs);
        // Move player to the exit position
        if (exitNumber != -1)
        {
            LocationExit exit = currentLocation.exits[exitNumber];
            print(exit);
            Player.instance.transform.position = VectorHelper.SetZ(exit.transform.position, -10);
            Player.instance.animation.PlayState(Animations.IDLE + exit.idleSide);
        }
        else
        {
            Player.instance.transform.position = VectorHelper.SetZ(playerPosition, -10);
        }
        Player.instance.gameObject.SetActive(true);
        // Save the game
        if (saveType != SaveType.None)
            Saves.Save();
        // Play music
        Music.instance.PlayBackground(currentLocation.music);
        // Show PlayerUI
        Player.instance.ui.Show();
        // Blend in
        Controls.freezedControls--;
        CameraFollow.instance.Reset();
        UIMessage.ShowMessage(currentLocation.locationName);
        for (float i = 1; i > 0.01f; i = Mathf.Lerp(i, 0, 5 * Time.deltaTime) - Time.deltaTime)
        {
            overlayState = i;
            yield return null;
        }
        overlayState = 0;
    }
}

public enum SaveType
{
    None,
    Local,
    Global
}
