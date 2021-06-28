using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPreparing : Transformer
{
    private const float PREPARING_TIME = 1;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _specie;

    private void Awake()
    {
        transform.SetParent(Locations.currentLocation.transform);
        if (_specie != null)
        {
            Init(_specie, false);
        }
    }

    public void Init(GameObject specie, bool lookAtPlayer)
    {
        CombatArea.current.enemies.Add(gameObject);
        _specie = specie;
        EnemyMovement template = _specie.GetComponent<EnemyMovement>();
        _spriteRenderer.sprite = template.spriteRenderer.sprite;
        if (lookAtPlayer)
            _spriteRenderer.flipX = Player.instance.transform.position.x > transform.position.x;
        StartCoroutine(Prepare());
    }

    private IEnumerator Prepare()
    {
        float startingTime = Time.time;
        while (true)
        {
            float alpha = 0.5f + 0.5f * (Mathf.Sin(Time.time * 24) + 1) / 2;
            _spriteRenderer.color = new Color(1, 1, 1, alpha);
            if (Time.time - startingTime >= PREPARING_TIME)
                break;
            yield return null;
        }
        Instantiate(_specie, transform.position, Quaternion.identity);
        CombatArea.current.enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}
