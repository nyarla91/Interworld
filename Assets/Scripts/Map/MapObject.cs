using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    [SerializeField] protected string[] descriptionVariants;
    protected string description;

    protected virtual void OnMouseEnter()
    {
        if (descriptionVariants.Length > 0)
        {
            description = descriptionVariants[Localization.language];
        }
        MapObjectDescription.description = Localization.Translate(description);
    }
    
    private void OnMouseExit()
    {
        MapObjectDescription.description = "";
    }
}
