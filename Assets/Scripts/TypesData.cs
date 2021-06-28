using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypesData : MonoBehaviour
{
    public static TypesData instance;

    public GameObject energyDrop;

    public Sprite scrollAgility;
    public Sprite scrollAccuracy;
    public Sprite ringJuggler;
    public Sprite ringRogue;
    public Sprite ringGolem;
    public Sprite ringPyromancer;
    public Sprite ringSorcerer;
    public Sprite ringCryomancer;

    private void Awake()
    {
        instance = this;
    }
}
