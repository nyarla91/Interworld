using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapObjectDescription : MonoBehaviour
{
    public static MapObjectDescription instance;

    public static string description
    {
        get => instance._text.text;
        set
        {
            instance._text.text = value;
            instance._background.localScale = new Vector2(instance._text.preferredWidth +
                0.4f * BoolHelper.BoolToInt(instance._text.text.Length > 0), 0.8f);
        } 
    }

    [SerializeField] private TextMeshPro _text;

    [SerializeField] private Transform _background;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        transform.position = VectorHelper.SetZ(CameraProperties.mousePosition, - 50);
    }
}
