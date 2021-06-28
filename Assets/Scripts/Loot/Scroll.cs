using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : Loot, ILoot
{
    public const int SCROLLS_REQUIRED = 2;
    
    public delegate void CollectedHandler();

    public CollectedHandler OnCollected;
    protected override void Awake()
    {
        base.Awake();
        lootClass = this;
    }

    public void Collect()
    {
        Saves.Data.scrolls.Add(_spriteRenderer.sprite);
        if (Saves.Data.scrolls.FindAll(t => t.Equals(_spriteRenderer.sprite)).Count >= SCROLLS_REQUIRED)
        {
            Activate(_spriteRenderer.sprite);
        }
        if (OnCollected != null)
            OnCollected();
    }

    private void Activate(Sprite type)
    { 
        if (type.Equals(TypesData.instance.scrollAgility))
        {
            Saves.Data.dash = true;
        }
        else if (type.Equals(TypesData.instance.scrollAccuracy))
        {
            Saves.Data.knifeDistanceKoff = 1.5f;
        }
    }
}
