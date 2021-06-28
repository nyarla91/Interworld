using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Charger : EnemySpecies, IEnemy
{
    [Header("Stats")]
    [SerializeField] private float _chargeCooldown;
    [SerializeField] private float _chargeDelay;
    [SerializeField] private float _chargeDistance;
    [SerializeField] private float _chargeDuration;
    [SerializeField] private float _chargeSpeed;
    private bool _chargeReady = true;

    protected override void Awake()
    {
        base.Awake();
        status.speciesClass = this;
    }

    private void Start()
    {
        movement.Follow(Player.instance.transform);
        animation.PlayState(Animations.WALK);
    }

    private void Update()
    {
        if (pathToPlayer < _chargeDistance && _chargeReady)
        {
            StartCoroutine(Charge());
        }
    }

    private IEnumerator Charge()
    {
        // Stop
        _chargeReady = false;
        float oldSpeed = movement.speed;
        movement.speed = 0;
        animation.PlayState(Animations.IDLE);
        
        // Charge
        yield return new WaitForSeconds(_chargeDelay);
        movement.speed = _chargeSpeed;
        
        // Return to walk
        yield return new WaitForSeconds(_chargeDuration);
        movement.speed = oldSpeed;
        movement.Follow(Player.instance.transform);
        animation.PlayState(Animations.WALK);
        
        // Cooldown
        yield return new WaitForSeconds(_chargeCooldown);
        _chargeReady = true;
    }

    public void OnKnifeHit(Vector2 directionFrom)
    {
        status.Die();
    }
}