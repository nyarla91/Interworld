using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DarkCaster : EnemySpecies, IEnemy
{
    [SerializeField] private GameObject _spellPrefab;

    [Header("Properties")]
    [SerializeField] private float _wavesCooldown;
    [SerializeField] private int _spellsPerWave;
    [SerializeField] private float _spellsCooldown;
    [SerializeField] private float _spellSpeed;
    [SerializeField] private float _approachDistance;

    private bool _immune;

    private void Start()
    {
        animation.tint = Color.gray;
        _immune = true;
        status.speciesClass = this;
        StartCoroutine(Spellcasting());
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (distanceToPlayer > _approachDistance)
        {
            movement.Follow(Player.instance.transform);
            animation.PlayState(Animations.WALK);
            return;
        }
        movement.Stop();
        animation.PlayState(Animations.IDLE);
    }

    private IEnumerator Spellcasting()
    {
        yield return new WaitForSeconds(Random.Range(0.2f, 1.2f));
        while (true)
        {
            animation.tint = Color.white;
            _immune = false;
            for (int i = 0; i < _spellsPerWave; i++)
            {
                Projectile.Create<EnemyProjectile>(_spellPrefab, transform.position, directiontoPlayer, _spellSpeed);
                yield return new WaitForSeconds(_spellsCooldown);
            }
            animation.tint = Color.gray;
            _immune = true;
            yield return new WaitForSeconds(_wavesCooldown);
        }
    }

    public void OnKnifeHit(Vector2 directionFrom)
    {
        if (! _immune)
            status.Die();
    }
}
