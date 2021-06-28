using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : Loot, ILoot
{
    [SerializeField] private RingInfo _info = new RingInfo();

    public Scroll.CollectedHandler OnCollected;

    private void Start()
    {
        _info.type = _spriteRenderer.sprite;
        lootClass = this;
    }
    
    public void Collect()
    {
        if (OnCollected != null)
            OnCollected();
        Saves.Data.rings.Add(_info);
        Saves.Save();
    }
}

[Serializable]
public class RingInfo
{
    [HideInInspector] public Sprite type;
    public string[] ringName;
    public int power;
    [TextArea(5, 10)] public string[] description;
}