using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Collider2D))]
public class CameraTrigger : Transformer
{
    [SerializeField] private Vector2 _anchor;
    [Range(0, 1)] [SerializeField] private float _interpolation;
    [SerializeField] private float _size = 5;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            CameraFollow.instance.ApplySettings((Vector2) transform.position + _anchor, _interpolation, _size);
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere( VectorHelper.SetZ(gameObject.transform.position + (Vector3) _anchor, -10), 0.2f);
    }
}
