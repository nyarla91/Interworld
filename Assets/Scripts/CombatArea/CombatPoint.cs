using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPoint : Transformer
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public List<GameObject> species;
    public bool lookAtPlayer;

    protected override void Awake()
    {
        base.Awake();
        Destroy(_spriteRenderer);
    }
}
