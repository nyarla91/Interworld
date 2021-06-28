using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityBook : Loot, ILoot
{
    protected override void Awake()
    {
        base.Awake();
        lootClass = this;
    }

    public void Collect()
    {
        Saves.Data.sanity++;
    }
}
