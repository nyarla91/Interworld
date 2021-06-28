using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WallShooter : Transformer
{
    [SerializeField] private GameObject _dartPrefab;
    [SerializeField] private Transform _emmitePoint;
    [SerializeField] private float _period;
    [SerializeField] private float _offset;
    [SerializeField] private float _dartSpeed;
    [SerializeField] private string _turnOffTrigger;

    private List<GameObject> _projectiles = new List<GameObject>();

    private void Awake()
    {
        Events.OnEvent += TurnOff;
    }

    private void Start()
    {
        StartCoroutine(Shoot());
        if (Vector2.Distance(Player.instance.transform.position, transform.position) < 50)
        {
            string log = name + "\n";
            Vector2 raycastdirection = (_emmitePoint.transform.position - transform.position).normalized;
            LayerMask mask = LayerMask.GetMask("Wall");
            RaycastHit2D raycast = Physics2D.Raycast(_emmitePoint.position, raycastdirection, 50, mask);
            log += "Space: " + (_period * _dartSpeed) + "\n";
            log += "Offset space: " + (_offset * _dartSpeed) + "\n";
            log += "RaycastDistance: " + raycast.distance + "\n";
            log += $"i: ({raycast.distance} - {_offset} * {_dartSpeed}) / {_period} * {_dartSpeed} = {((raycast.distance - _offset * _dartSpeed) / _period * _dartSpeed)}\n";
            for (int i = (int) ((raycast.distance - _offset * _dartSpeed) / _period / _dartSpeed); i > 0; i--)
            {
                log += "Interation " + i + ":\n";
                Vector3 origin = _emmitePoint.transform.position;
                Vector3 direction = VectorHelper.DegreesToVector2(transform.rotation.eulerAngles.z);
                Vector3 position = origin + direction * _dartSpeed * (_period * i + _offset);
                log += "Offset position: " + (position - origin) + "\n";
                EnemyProjectile newDart = 
                    Projectile.Create<EnemyProjectile>(_dartPrefab, position, Vector2.zero, _dartSpeed);
                newDart.degreesDirection = transform.rotation.eulerAngles.z;
                _projectiles.Add(newDart.gameObject);
            }
            //Debug.Log(log);
        }
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(_offset);
        while (true)
        {
            if (Vector2.Distance(transform.position, Player.instance.transform.position) < 30)
            {
                EnemyProjectile newDart = 
                    Projectile.Create<EnemyProjectile>(_dartPrefab, _emmitePoint.position, Vector2.zero, _dartSpeed);
                newDart.degreesDirection = transform.rotation.eulerAngles.z;
                _projectiles.Add(newDart.gameObject);
            }
            yield return new WaitForSeconds(_period);
        }
    }

    private void TurnOff(string eventType)
    {
        if (eventType.Equals(_turnOffTrigger))
        {
            foreach (var projectile in _projectiles)
            {
                Destroy(projectile);
            }
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        Events.OnEvent -= TurnOff;
    }
}
