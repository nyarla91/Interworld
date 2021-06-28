using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrop : Loot
{
    private void Start()
    {
        if (Saves.QuickData.lootCollected.Contains(name))
            Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>() != null && PlayerRings.energy < 5)
        {
            Saves.QuickData.lootCollected.Add(name);
            PlayerRings.energy++;
            Destroy(gameObject);
        }
    }
}