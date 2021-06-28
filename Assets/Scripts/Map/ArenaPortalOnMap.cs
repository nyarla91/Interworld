using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaPortalOnMap : MapObject
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private LocationExit _reference;

    private void Awake()
    {
        if (Saves.Data.arenasCleared.Contains(_reference.gameObject.name))
        {
            for (int i = 0; i < descriptionVariants.Length; i++)
            {
                descriptionVariants[i] += Localization.Translate("<mapCleared>");
            }
            _spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        }
    }
}
