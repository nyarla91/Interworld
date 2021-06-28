using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static Menu instance;
    
    [SerializeField] private CanvasGroup _canvasGroup;

    public delegate void ToggleHandler();
    public static ToggleHandler OnToggle;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Controls.instance.keyMenu))
            Toggle(!_canvasGroup.blocksRaycasts);
            
    }

    public void Toggle(bool open)
    {
        if (open && UIWindows.windowOpen)
            return;
        if (OnToggle != null)
            OnToggle();
        UIWindows.windowOpen =_canvasGroup.blocksRaycasts = _canvasGroup.interactable = open;
        _canvasGroup.alpha = BoolHelper.BoolToInt(open);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
