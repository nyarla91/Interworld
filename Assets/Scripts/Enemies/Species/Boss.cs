using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : EnemySpecies, IEnemy
{
    public static Boss current;
    
    [SerializeField] private GameObject _shieldPrefab;
    [SerializeField] private GameObject _heartPrefab;
    [SerializeField] private GameObject _spellPrefab;
    [SerializeField] private int hearts;
    [SerializeField] private float heartsSpeed;
    [SerializeField] private float heartsDistance;
    
    [HideInInspector] public List<BossHeart> _hearts = new List<BossHeart>();

    private GameObject _shield;
    
    protected override void Awake()
    {
        base.Awake();
        status.speciesClass = this;
        current = this;
    }

    private void Start()
    {
        for (int i = 0; i < hearts; i++)
        {
            _hearts.Add(Instantiate(_heartPrefab, transform.position, Quaternion.identity ).GetComponent<BossHeart>());
            _hearts[_hearts.Count - 1].transform.SetParent(transform);
        }
        _shield = Instantiate(_shieldPrefab, transform.position + Vector3.back, Quaternion.identity);
        _shield.transform.SetParent(transform);
        StartCoroutine(Shooting());
    }

    private void Update()
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            if (_hearts[i] == null)
            {
                _hearts.RemoveAt(i);
                continue;
            }
            float z = 360 / _hearts.Count * i + Time.time * heartsSpeed % 360;
            Vector3 position = VectorHelper.SetZ(VectorHelper.DegreesToVector2(z) * heartsDistance, transform.position.z);
            _hearts[i].transform.position = Vector3.Lerp(_hearts[i].transform.position, transform.position + position, Time.deltaTime * 5);
        }

        if (_hearts.Count == 0 && CombatArea.current.enemies.Count <= 1 && _shield != null)
        {
            Destroy(_shield);
            Corpse newCorpse = Instantiate(status.corpsePrefab, transform.position, Quaternion.identity).GetComponent<Corpse>();
            newCorpse.Init(_shield.GetComponent<SpriteRenderer>().sprite, false, 1);
        }
    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            float directionToPlayer = VectorHelper.Vector2ToDegrees(Player.instance.transform.position - transform.position);
            int attackType = Random.Range(1, 3);
            switch (attackType)
            {
                // Circle
                case 1:
                {
                    int projectiles = 12;
                    float offset = Random.value * 360;
                    for (int i = 0; i < projectiles; i++)
                    {
                        Shoot(360 / projectiles * i + offset);
                    }
                    break;
                }
                // Arc
                case 2:
                {
                    int projectiles = 9;
                    float arc = 90;
                    for (float i = -arc/2; i < arc/2; i += arc / projectiles)
                    {
                        Shoot(directionToPlayer + i);
                    }
                    break;
                }
                default:
                {
                    continue;
                }
            }
            yield return new WaitForSeconds(3);
        }
    }

    private void Shoot(float direction)
    {
        Projectile.Create<EnemyProjectile>(_spellPrefab, transform.position, VectorHelper.DegreesToVector2(direction), 2);
    }

    public void OnKnifeHit(Vector2 directionFrom)
    {
        if (_shield == null)
        {
            Saves.Data.bossDeathPosition = transform.position;
            status.Die();
        }
    }
}
