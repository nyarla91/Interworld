using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map current;
    
    private const float MAP_SCALE_DELTA = 0.8f;
    [SerializeField] private Transform mapObject;
    
    private bool _opened;

    private void Awake()
    {
        current = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Controls.instance.keyMap) && Saves.Data.map)
        {
            ToggleMap(!_opened);
        }
        if (Input.GetMouseButtonDown(0) && _opened)
            StartCoroutine(DragMap());
    }

    public void ToggleMap(bool opened)
    {
        // Prevent from openning if other window is open
        if (UIWindows.windowOpen && opened)
            return;
        // Show map
        UIWindows.windowOpen = opened;
        _opened = opened;
        mapObject.gameObject.SetActive(_opened);
        // Freeze map
        CameraFollow.instance.freezed = opened;
        // Set player position on map
        if (opened)
            PlayerOnMap.current.transform.position = VectorHelper.SetZ(Player.instance.transform.position, -31);
        // Scale map
        float scale = 1 - BoolHelper.BoolToInt(_opened) * MAP_SCALE_DELTA;
        // Move map to the center of screen
        if (opened)
        {
            Vector3 playerOnMapPosition = PlayerOnMap.current.transform.position * (1 - MAP_SCALE_DELTA);
            Vector2 delta = CameraProperties.mainCamera.transform.localPosition - playerOnMapPosition;
            mapObject.position = VectorHelper.SetZ(mapObject.localPosition + (Vector3) delta, -20);
        }
        else
        {
            mapObject.position = Vector3.zero;
            // Hide description
            MapObjectDescription.description = "";
        }
        //PlayerOnMap.current.transform.position = VectorHelper.SetZ(PlayerOnMap.current.transform.position, -2);
        MapBackground.opened = _opened;
        mapObject.localScale = new Vector3(scale, scale, scale);
    }

    private IEnumerator DragMap()
    {
        Vector3 anchor = mapObject.position;
        Vector3 mouseAnchor = CameraProperties.mousePosition;
        while (true)
        {
            Vector3 mouseDelta = (CameraProperties.mousePosition - mouseAnchor) * 0.8f;
            Vector3 newPosition = VectorHelper.SetZ(anchor + mouseDelta, -21);
            mapObject.position = newPosition;
            if (!Input.GetMouseButton(0))
                yield break;
            yield return null;
        }
    }
}
