using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public delegate void EventHandler(string eventType);
    public static EventHandler OnEvent;

    private void Awake()
    {
        OnEvent += type => {print("Event called: " + type);};
    }
}
