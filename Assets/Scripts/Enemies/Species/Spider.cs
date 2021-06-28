using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : EnemySpecies, IEnemy
{
    [SerializeField] protected float _topSpeed;
    [SerializeField] protected float _speedPerSecond;
    
    protected override void Awake()
    {
        base.Awake();
        status.speciesClass = this;
    }

    private void Start()
    {
        animation.PlayState(Animations.IDLE);
        movement.Follow(Player.instance.transform);
        StartCoroutine(Acceleration());
    }

    private IEnumerator Acceleration()
    {
        while (movement.speed < _topSpeed)
        {
            movement.speed += _speedPerSecond * Time.deltaTime;
            yield return null;
        }
        movement.speed = _topSpeed;
    }

    public virtual void OnKnifeHit(Vector2 directionFrom)
    {
        status.Die();
    }
}
