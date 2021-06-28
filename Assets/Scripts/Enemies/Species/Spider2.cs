using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider2 : Spider
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _birthPrefab;
    
    protected override void Awake()
    {
        base.Awake();
        status.speciesClass = this;
        status.OnDeath += SpidersBirth;
    }
    public void SpidersBirth()
    {
        for (float i = -0.2f; i <= 0.2f; i += 0.4f)
        {
            Vector3 position = transform.position + Vector3.right * i;
            EnemyPreparing newEnemy = Instantiate(_enemyPrefab, position, Quaternion.identity).GetComponent<EnemyPreparing>();
            newEnemy.Init(_birthPrefab, true);
        }
    }
}
