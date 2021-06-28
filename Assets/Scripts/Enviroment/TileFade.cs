using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileFade : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private bool _visible;

    private Coroutine fadeCoroutine;

    private void Start()
    {
        _tilemap.color = new Color(1, 1, 1, BoolHelper.BoolToInt(_visible));
    }

    public virtual void FadeTiles(bool fadeIn, float speed)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(Fade(fadeIn, speed));
    }

    private IEnumerator Fade(bool fadeIn, float speed)
    {
        float targetAlpha = BoolHelper.BoolToInt(fadeIn);
        while (Mathf.Abs(_tilemap.color.a - targetAlpha) > 0.05f)
        {
            _tilemap.color = VectorHelper.SetA(Color.white, Mathf.Lerp(_tilemap.color.a, targetAlpha, speed * Time.deltaTime));
            yield return null;
        }
        _tilemap.color = VectorHelper.SetA(Color.white, targetAlpha);
    }
}
