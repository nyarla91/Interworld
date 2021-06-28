using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CombatArea : MonoBehaviour
{
    public static CombatArea current;
    public static bool combat
    {
        get
        {
            if (current != null)
                return current._combatCoroutine != null;
            return false;
        }
    }

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private bool _save;
    [SerializeField] private bool _arena;
    [SerializeField] private bool _boss;
    [SerializeField] private CombatWalls _walls;
    [SerializeField] private List<CombatWave> waves;

    [HideInInspector] public bool nextBossWave;
    
    public List<GameObject> enemies = new List<GameObject>();
    private Coroutine _combatCoroutine;

    private void Awake()
    {
        if (Saves.QuickData.combatAreasCleared.Contains(name) || (_boss && Saves.Data.bossKilled))
        {
            foreach (var wave in waves)
            {
                if (wave.eventCall.Length > 0)
                    Events.OnEvent(wave.eventCall);
            }
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() != null && !combat && !Saves.QuickData.combatAreasCleared.Contains(name))
        {
            _combatCoroutine = StartCoroutine(Combat());
        }
    }

    private IEnumerator Combat()
    {
        current = this;
        _walls.FadeTiles(true);
        int i = 0;
        Music.instance.PlayOverride(Music.instance.battleMusic);
        foreach (var wave in waves)
        {
            if (wave.points.Count > 0)
            {
                foreach (var point in wave.points)
                {
                    GameObject specie = CollectionHelper.ChooseRandomElement(point.species);
                    EnemyPreparing newEnemy = Instantiate(_enemyPrefab, point.transform.position, Quaternion.identity).GetComponent<EnemyPreparing>();
                    newEnemy.Init(specie, point.lookAtPlayer);
                }
                if (!_boss)
                    yield return new WaitUntil(() => enemies.Count <= 0);
                else
                {
                    yield return new WaitUntil(() => nextBossWave || (Boss.current != null && Boss.current._hearts.Count == 0));
                    nextBossWave = false;
                }
                if (wave.eventCall.Length > 0)
                    Events.OnEvent(wave.eventCall);
            }
            i++;
        }
        if (_boss)
            yield return new WaitUntil(() => enemies.Count == 0);
        _combatCoroutine = null;
        _walls.FadeTiles(false);
        Saves.QuickData.combatAreasCleared.Add(name);
        if (_boss)
            Saves.Data.bossKilled = true;
        if (_arena)
            Saves.Data.arenasCleared.Add(Arena.current);
        if (_save)
            Saves.Save();
        if (_boss)
            Events.OnEvent("bossDefeat");
        Music.instance.StopOverride();
        Destroy(this);
    }

    public void InterruptCombat()
    {
        if (_combatCoroutine != null)
        {
            StopCoroutine(_combatCoroutine);
            nextBossWave = false;
            _combatCoroutine = null;
            _walls.FadeTiles(false);
            foreach (var enemy in enemies)
            {
                Destroy(enemy);
            }
        }
        
    }

    public void CryomancerRingEffect(bool immobolize)
    {
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<EnemyMovement>().Immobilize(immobolize);
        }
    }
    
    [Serializable]
    public class CombatWave
    {
        public string eventCall;
        public List<CombatPoint> points;
    }
}

