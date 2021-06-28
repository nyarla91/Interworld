using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    [SerializeField] protected bool collideToPlayer = true;
    [SerializeField] protected int damage = 1;

    protected override void Awake()
    {
        base.Awake();
        transform.SetParent(Locations.currentLocation.transform);
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        if (collideToPlayer)
        {
            PlayerStatus playerStatus = other.gameObject.GetComponent<PlayerStatus>();
            if (playerStatus != null)
            {
                playerStatus.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
