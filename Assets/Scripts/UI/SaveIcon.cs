using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveIcon : UIAlpha
{
    public static SaveIcon instance;

    private Coroutine _blinkCoroutine;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public void Save()
    {
        if (_blinkCoroutine != null)
            StopCoroutine(_blinkCoroutine);
        _blinkCoroutine = StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        for (int i = 0; i < 2; i++)
        {
            Show();
            yield return new WaitForSeconds(0.2f);
            Hide();
            yield return new WaitForSeconds(0.2f);
        }
    }
    
}
