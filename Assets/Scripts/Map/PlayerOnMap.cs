using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnMap : MapObject
{
    public static PlayerOnMap current;

    private void Awake()
    {
        current = this;
    }
}
