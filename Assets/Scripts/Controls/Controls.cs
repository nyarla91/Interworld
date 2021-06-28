using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public static Controls instance;

    public static Vector2 moveAxis
    {
        get
        {
            if (freezedControls == 0)
                return new Vector2
                (
                    BoolHelper.BoolToInt(Input.GetKey(instance.keyMoveRight)) + 
                    BoolHelper.BoolToInt(Input.GetKey(instance.keyMoveLeft) ) * -1,
                    BoolHelper.BoolToInt(Input.GetKey(instance.keyMoveUp)) + 
                    BoolHelper.BoolToInt(Input.GetKey(instance.keyMoveDown) ) * -1
                ).normalized;
            return Vector2.zero;
        }
    }
    
    public KeyCode keyMoveLeft;
    public KeyCode keyMoveRight;
    public KeyCode keyMoveUp;
    public KeyCode keyMoveDown;
    public KeyCode keyInteract;
    public KeyCode keyDash;
    public KeyCode keyPullKnives;
    public KeyCode keyHealingPotion;
    public KeyCode keyRings;
    public KeyCode keyMap;
    public KeyCode keyMenu;
    public KeyCode keyTeleport;
    public KeyCode keyNextSentence;

    private static int _freezedControls;
    public static int freezedControls
    {
        get => _freezedControls;
        set => _freezedControls = Mathf.Clamp(value, 0, int.MaxValue);
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        Application.targetFrameRate = 300;
    }

    public static Vector2 DirectionFromPosition(Vector2 position) => ((Vector2) CameraProperties.mousePosition - position).normalized;
}
