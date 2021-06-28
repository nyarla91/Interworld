using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.U2D;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody2D))]
public class Loot : Transformer
{
    [SerializeField] protected bool _permanent;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    
    protected int additionalValue;
    protected ILoot lootClass;

    private float _naturalScale = -1;

    private float naturalScale
    {
        get
        {
            if (_naturalScale == -1)
                _naturalScale = transform.localScale.x;
            return _naturalScale;
        }
    }

    protected override void Awake()
    {
        if (_permanent && Saves.Data.lootCollected.Contains(name))
            Destroy(gameObject);
        base.Awake();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            if (_permanent)
            {
                Saves.Data.lootCollected.Add(name);
                Saves.Save();
            }
            lootClass.Collect();
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.localScale = (naturalScale + Mathf.Sin(Time.time * 3f) * 0.1f) * Vector3.one;
    }

    protected void RandomizeAngle() =>
        transform.rotation = Quaternion.Euler(0, 0, Random.value * 360);

    public static void Spawnloot(GameObject prefab, Vector3 position, float zAngle, int additionalValue)
    {
        Loot newLoot = Instantiate(prefab, position, Quaternion.Euler(0, 0, zAngle)).GetComponent<Loot>();
        if (newLoot != null)
        {
            newLoot.additionalValue = additionalValue;
        }
    }
}