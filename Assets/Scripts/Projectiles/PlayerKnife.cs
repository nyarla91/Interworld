using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnife : Projectile
{
    [SerializeField] private GameObject lootPrefab;
    public const float Z_ROTATION_OFFSET = -45;
    private const float SLOWING_KOFF = 0.4f;

    [HideInInspector] public bool available = true;

    private void Start()
    {
        rotateToDirectionOffset = Z_ROTATION_OFFSET;
    }

    private void FixedUpdate()
    {
        speed = Mathf.Lerp(speed, 0, SLOWING_KOFF);
        if (speed > 0 && speed < 0.1f)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Loot.Spawnloot(lootPrefab, transform.position, transform.rotation.eulerAngles.z, 0);
    }
}
