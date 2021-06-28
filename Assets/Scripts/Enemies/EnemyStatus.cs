using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : Transformer
{
    public GameObject corpsePrefab;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public IEnemy speciesClass;

    public delegate void DeathHandler();

    public DeathHandler OnDeath;

    protected override void Awake()
    {
        base.Awake();
        CombatArea.current.enemies.Add(gameObject);
        transform.SetParent(Locations.currentLocation.transform);
    }

    public void Die()
    {
        if (OnDeath != null)
            OnDeath();
        CombatArea.current.enemies.Remove(gameObject);
        Corpse.SpawnCorpse(corpsePrefab, transform.position, _spriteRenderer.sprite, _spriteRenderer.flipX, transform.localScale.x);
        Sound.Play(deathSound, 0.3f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerKnife otherKnife = other.gameObject.GetComponent<PlayerKnife>();
        if (otherKnife != null)
        {
            Destroy(other.gameObject);
            speciesClass.OnKnifeHit((other.gameObject.transform.position - transform.position).normalized);
        }
    }
}
