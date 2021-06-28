using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : Loot
{
    private void Start()
    {
        if (Saves.QuickData.lootCollected.Contains(name))
            Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>() != null && PlayerStatus.healingPotions < 3)
        {
            Saves.QuickData.lootCollected.Add(name);
            PlayerStatus.healingPotions++;
            Destroy(gameObject);
        }
    }
}
