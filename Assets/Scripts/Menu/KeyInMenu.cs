using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyInMenu : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        Menu.OnToggle += () =>
        {
            int keys = Saves.Data.keys.FindAll(t => t.Equals(_image.sprite)).Count;
            _textMeshPro.text = $"{keys}";
            _canvasGroup.alpha = BoolHelper.BoolToInt(keys > 0);
        };
    }
}
