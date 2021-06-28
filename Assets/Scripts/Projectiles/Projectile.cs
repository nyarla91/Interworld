using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : Transformer
{
    [SerializeField] private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    public Vector2 direction
    {
        get => _direction;
        set
        {
            _direction = value.normalized;
            CountVelocity();
        }
    }

    public float degreesDirection
    {
        get => VectorHelper.Vector2ToDegrees(direction);
        set
        {
            direction = VectorHelper.DegreesToVector2(value);
            CountVelocity();
        }
    }

    private float _speed;

    public float speed
    {
        get => _speed;
        set
        {
            _speed = value;
            CountVelocity();
        }
    }

    [SerializeField] protected bool destroyOnWalls = true;
    [SerializeField] protected bool rotateToDirection = true;
    [SerializeField] protected float rotateToDirectionOffset;

    public void SetDirectionFromDegrees(float z)
    {
        direction = VectorHelper.DegreesToVector2(z);
    }

    private void CountVelocity()
    {
        _rigidbody.velocity = direction * speed;
        if (rotateToDirection)
            transform.rotation = quaternion.Euler(0, 0, (VectorHelper.Vector2ToDegrees(direction)+ rotateToDirectionOffset) * Mathf.Deg2Rad);
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == (int) Layer.Wall && destroyOnWalls)
        {
            Destroy(gameObject);
        }
    }

    public static T Create<T>(GameObject prefab, Vector2 position, Vector2 direction, float speed)
    {
        if (prefab.GetComponent<Projectile>() == null)
            throw new Exception($"{prefab.name} must contain Projectile class");
        Vector3 position3 = VectorHelper.SetZ(position, -5);
        Projectile newProjectile = Instantiate(prefab, position3, Quaternion.identity).GetComponent<Projectile>();
        newProjectile.direction = direction;
        newProjectile.speed = speed;
        return newProjectile.GetComponent<T>();
    }
}
