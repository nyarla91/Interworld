using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBackground : MonoBehaviour
{
    public static MapBackground instance;

    public static bool opened
    {
        get => instance.background.activeSelf;
        set => instance.background.SetActive(value);
    }
    
    [SerializeField] private GameObject background;

    private void Awake()
    {
        instance = this;
    }
}
