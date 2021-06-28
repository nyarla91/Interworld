using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : EnemySpecies, IEnemy
{
    [SerializeField] private float _closestDistance;
    [SerializeField] private float _furtherDistance;
    private float _alpha = 1.2f;
    private float _playerNearAlpha;

    private void Start()
    {
        status.speciesClass = this;
        movement.Follow(Player.instance.transform);
        animation.PlayState(Animations.WALK);
        StartCoroutine(Disappear());
    }

    private void Update()
    {
        _playerNearAlpha = 1 - MathHelper.Evaluate(pathToPlayer, _closestDistance, _furtherDistance);
        animation.alpha = _alpha + _playerNearAlpha;
    }

    private IEnumerator Disappear()
    {
        while (_alpha > 0)
        {
            _alpha -= Time.deltaTime * 1;
            yield return null;
        }
        _alpha = 0;
    }

    public void OnKnifeHit(Vector2 directionFrom)
    {
        if (_alpha + _playerNearAlpha > 0)
            status.Die();
    }
}
