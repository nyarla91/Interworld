using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAlpha : Transformer
{
    [SerializeField] protected CanvasGroup _canvasGroup;
    [SerializeField] protected float _toggleSpeed = 10;

    private Coroutine _alphaCoroutine;

    public void Show()
    {
        if (_alphaCoroutine != null)
            StopCoroutine(_alphaCoroutine);
        _alphaCoroutine = StartCoroutine(Alpha(1));
        _canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
    }

    public void Hide()
    {
        if (_alphaCoroutine != null)
            StopCoroutine(_alphaCoroutine);
        _alphaCoroutine = StartCoroutine(Alpha(0));
        _canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
    }

    private IEnumerator Alpha(float target)
    {
        while (Mathf.Abs(_canvasGroup.alpha - target) > 0.02f)
        {
            _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, target, Time.deltaTime * _toggleSpeed);
            yield return null;
        }
        _canvasGroup.alpha = target;
    }
}
