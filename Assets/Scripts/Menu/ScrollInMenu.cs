using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollInMenu : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        Menu.OnToggle += () =>
        {
            int scrolls = Saves.Data.scrolls.FindAll(t => t.Equals(_image.sprite)).Count;
            if (scrolls < Scroll.SCROLLS_REQUIRED)
            {
                _textMeshPro.text = $"{scrolls}/{Scroll.SCROLLS_REQUIRED}";
                _image.color = new Color(1, 1, 1, 0.7f);
            }
            else
            {
                _textMeshPro.text = "";
                _image.color = Color.white;
            }
            _canvasGroup.alpha = BoolHelper.BoolToInt(scrolls > 0);
        };
    }
}