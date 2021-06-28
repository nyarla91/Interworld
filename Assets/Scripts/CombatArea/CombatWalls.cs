using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CombatWalls : TileFade
{
    [SerializeField] private TilemapCollider2D _collider;

    private void Awake()
    {
        _collider.enabled = false;
    }

    public void FadeTiles(bool fadeIn)
    {
        _collider.enabled = fadeIn;
        FadeTiles(fadeIn, 3);
    }
}
