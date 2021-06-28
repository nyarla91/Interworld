using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLockOnMap : MapObject
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private EventLock _reference;

    private bool _unlocked;
    
    private void Awake()
    {
        _reference.OnUnlock += Unlock;
    }

    private void Unlock()
    {
        _unlocked = true;
        _spriteRenderer.color = VectorHelper.SetA(_spriteRenderer.color, 0.5f);
    }
}
