using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPlayerKnife : Loot, ILoot
{
    private void Start()
    {
        lootClass = this;
        Player.instance.weapon.lootKnives.Add(transform);
    }
    
    public void Collect()
    {
        Player.instance.weapon.lootKnives.Remove(transform);
        Player.instance.weapon.knives++;
    }
}
