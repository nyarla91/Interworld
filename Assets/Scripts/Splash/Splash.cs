using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Color _backgroundColor;
    [SerializeField] private Image _image;
    [SerializeField] private Splash _nextSplash;
    [SerializeField] private bool _starting;
    [SerializeField] private bool _nextScene;
    
    private float alpha
    {
        get => _image.color.a;
        set => _image.color = VectorHelper.SetA(_image.color, value);
    }

    private void Awake()
    {
        if (_starting)
        {
            Show();
        }
    }

    public void Show()
    {
        StartCoroutine(Lifetime());
    }

    private IEnumerator Lifetime()
    {
        _background.color = _backgroundColor;
        for (float i = 0; i < 1; i += Time.deltaTime * 2)
        {
            alpha = i;
            yield return null;
        }
        alpha = 1;
        yield return new WaitForSeconds(2);
        for (float i = 0; i < 1; i += Time.deltaTime * 2)
        {
            alpha = 1 - i;
            yield return null;
        }
        alpha = 0;
        yield return new WaitForSeconds(0.5f);
        if (_nextScene)
        {
            Saves.LoadConfig();
            if (Saves.Config.loaded)
            {
                SceneManager.LoadScene("Location");
            }
            else
            {
                SceneManager.LoadScene("LanguageChoose");
            }
        }
        if (_nextSplash != null)
            _nextSplash.Show();
    }
}
