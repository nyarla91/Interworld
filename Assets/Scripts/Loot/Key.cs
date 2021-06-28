using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class Key : Loot, ILoot
{
    protected override void Awake()
    {
        base.Awake();
        lootClass = this;
    }

    public void Collect()
    {
        Saves.Data.keys.Add(_spriteRenderer.sprite);
        Saves.Save();
    }
}
