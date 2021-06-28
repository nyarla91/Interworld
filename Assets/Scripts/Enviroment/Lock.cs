using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Interactable
{
    public SpriteRenderer spriteRenderer;
    [SerializeField] protected GameObject _corpsePrefab;
    
    public delegate void UnlockHandler();
    public UnlockHandler OnUnlock;

    protected override void Awake()
    {
        if (Saves.Data.locksOpen.Contains(name) || Saves.test)
        {
            if (OnUnlock != null)
                OnUnlock();
            Destroy(gameObject);
        }
    }
}
