using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeart : MonoBehaviour
{
    [SerializeField] private GameObject _corpsePrefab;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == (int) Layer.PlayerProjectile)
        {
            Destroy(other.gameObject);
            Hit();
        }
    }

    public void Hit()
    {
        CombatArea.current.nextBossWave = true;
        Corpse.SpawnCorpse(_corpsePrefab, transform.position, _spriteRenderer.sprite, _spriteRenderer.flipX, transform.localScale.x);
        Destroy(gameObject);
    }
}
