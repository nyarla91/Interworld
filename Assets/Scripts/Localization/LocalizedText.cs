using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMeshPro;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] [TextArea(5, 10)] private string[] _text;

    private void Start()
    {
        string translated = _text[Localization.language];
        if (_textMeshPro != null)
            _textMeshPro.text = translated;
        if (_textMeshProUGUI != null)
            _textMeshProUGUI.text = translated;
    }
}
