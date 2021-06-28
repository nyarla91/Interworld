using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysList : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    private void Awake()
    {
        Menu.OnToggle += () => { _canvasGroup.alpha = BoolHelper.BoolToInt(Saves.Data.keys.Count > 0); };
    }
}
