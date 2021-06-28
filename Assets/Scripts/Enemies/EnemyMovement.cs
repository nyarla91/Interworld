using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyMovement : Transformer
{
    public SpriteRenderer spriteRenderer;
    public AIPath aiPath;
    [SerializeField] private AIDestinationSetter _aiDestination;
    [SerializeField] private Rigidbody2D _rigidbody;
    private bool _doesntMove;
    
    public float speed
    {
        get => aiPath.maxSpeed;
        set => aiPath.maxSpeed = value;
    }

    public float acceleration
    {
        get => aiPath.maxAcceleration;
        set => aiPath.maxAcceleration = value;
    }

    private Transform _dummyDestination;
    
    private Transform _destination
    {
        get => _aiDestination.target;
        set
        {
            aiPath.canMove = true;
            _aiDestination.target = value;
        } 
    }

    protected override void Awake()
    {
        base.Awake();
        _doesntMove = _rigidbody.constraints == RigidbodyConstraints2D.FreezeAll;
        _destination = _dummyDestination = new GameObject().transform;
        GetComponent<EnemyStatus>().OnDeath += () => { Destroy(_dummyDestination.gameObject); };
    }

    private void Update()
    {
        if (aiPath.desiredVelocity.x != 0)
            spriteRenderer.flipX = aiPath.desiredVelocity.x > 0;
    }

    public void SetDestinationPoint(Vector2 point)
    {
        _dummyDestination.position = point;
        _destination = _dummyDestination;
    }

    public void Follow(Transform target)
    {
        _destination = target;
    }

    public void Stop()
    {
        aiPath.canMove = false;
    }

    public void MoveDirectly(Vector2 move)
    {
        _rigidbody.MovePosition((Vector2) transform.position + move);
    }

    public void Immobilize(bool immobilize)
    {
        if (!_doesntMove)
        {
            if (immobilize)
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            else
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    
}
