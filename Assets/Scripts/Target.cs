using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private string eventType;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == (int) Layer.PlayerProjectile)
        {
            Events.OnEvent(eventType);
            Destroy(this);
        }
    }
}
