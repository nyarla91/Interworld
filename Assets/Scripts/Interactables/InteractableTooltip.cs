using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableTooltip : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public static InteractableTooltip instance;
    
    private void Awake()
    {
        instance = this;
    }

    public void MoveTo(Interactable target)
    {
        _spriteRenderer.enabled = true;
        transform.position = VectorHelper.SetZ(target.transform.position + new Vector3(0, 1, 0), transform.position.z);
    }

    public void Hide()
    {
        _spriteRenderer.enabled = false;
    }
}
