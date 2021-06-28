using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollsList : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    private void Awake()
    {
        Menu.OnToggle += () => { _canvasGroup.alpha = BoolHelper.BoolToInt(Saves.Data.scrolls.Count > 0); };
    }
}
