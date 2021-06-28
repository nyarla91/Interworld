using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindows : MonoBehaviour
{
    private static bool _windwoOpen;
    public static bool windowOpen
    {
        get => _windwoOpen || Tutorial.tutorial;
        set
        {
            if (_windwoOpen == value)
                return;
            _windwoOpen = value;
            Controls.freezedControls += BoolHelper.BoolToIntReversed(value);
        }
    }
}
