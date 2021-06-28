using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMessage : Transformer
{
    private static UIMessage instance;
    
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        instance = this;
    }

    public static void ShowMessage(string message)
    {
        instance.StopAllCoroutines();
        instance._textMeshPro.text = message;
        instance.StartCoroutine(Message(message.Split(new []{' '}).Length * 0.25f + 3));
    }

    private static IEnumerator Message(float duration)
    {
        instance._canvasGroup.alpha = 1;
        yield return new WaitForSeconds(duration);
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            instance._canvasGroup.alpha = i;
            yield return null;
        }
    }
}
