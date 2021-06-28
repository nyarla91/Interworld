using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour
{
    private Transform _transform;

    public new Transform transform
    {
        get
        {
            if (_transform == null)
                _transform = gameObject.transform;
            return _transform;
        }
        set => _transform = value;
    }
    public RectTransform rectTransform { get; protected set; }

    protected virtual void Awake()
    {
        if (gameObject.transform != null)
        {
            _transform = gameObject.transform;
        }

        if (GetComponent<RectTransform>() != null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }
}
