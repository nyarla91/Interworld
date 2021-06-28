using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class PlayerWeapon : Transformer
{
    [SerializeField] private GameObject _knifePrefab;
    [SerializeField] private GameObject _knifeAimingPrefab;
    [SerializeField] private float _startingImpulse;
    [SerializeField] private float _topImpulse;

    private int _knives = 3;
    private PlayerKnifeAiming _playerKnifeAiming;

    public int knives
    {
        get => _knives;
        set
        {
            _knives = value;
            Player.instance.ui.UpdateKnives(_knives);
        }
    }
    public List<Transform> lootKnives;

    public float impulsePercent => MathHelper.Evaluate(_impulse, _startingImpulse, _topImpulse);
    private float _impulse;

    private void Update()
    {
        if (Controls.freezedControls <= 0 && knives > 0 && Saves.Data.knives && Input.GetMouseButtonDown(0))
            StartCoroutine(Aiming());
        if (Controls.freezedControls <= 0 && Saves.Data.knives && Input.GetKeyDown(Controls.instance.keyPullKnives))
            StartCoroutine(PUllKnives());
    }

    private IEnumerator Aiming()
    {
        //Slow down
        Player.instance.movement.AddSpeedModifier("aiming", 0.4f);
        
        // Knife aim
        _playerKnifeAiming = Instantiate(_knifeAimingPrefab, transform.position, Quaternion.identity).GetComponent<PlayerKnifeAiming>();
        _playerKnifeAiming.Init(this, 0.5f);
        
        // Impulse raise
        _impulse = _startingImpulse;
        Coroutine coroutine = StartCoroutine(ImpulseRaise(_topImpulse, 0.2f));
        
        // Wait for release
        while (Input.GetMouseButton(0))
        {
            // Cancel throw with right click
            if (Input.GetMouseButtonDown(1))
            {
                StopAiming();
                yield break;
            }
            yield return null;
        }
        StopCoroutine(coroutine);
        
        // Destroy aim
        StopAiming();
        
        // Throw knife
        Vector2 knifePosition = (Vector2) transform.position + Controls.DirectionFromPosition(transform.position) * 0.5f;
        Vector2 knifeDirection = Controls.DirectionFromPosition(transform.position);
        Projectile.Create<PlayerKnife>(_knifePrefab, knifePosition, knifeDirection, _impulse * Saves.Data.knifeDistanceKoff);
        knives--;
        Sound.Play(CollectionHelper.ChooseRandomElement(Player.instance.sound.throwSounds), 0.7f);
    }

    private IEnumerator ImpulseRaise(float topImpulse, float tPerTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            _impulse = Mathf.Lerp(_impulse, topImpulse, tPerTime);
        }
    }

    public void StopAiming()
    {
        if (_playerKnifeAiming != null)
            Destroy(_playerKnifeAiming.gameObject);
        Player.instance.movement.CancelSpeedModifier("aiming");
    }

    private IEnumerator PUllKnives()
    {
        Controls.freezedControls++;
        float speed = 1f;
        while (Input.GetKey(Controls.instance.keyPullKnives))
        {
            if (speed < 20)
                speed *= 1 + 2 * Time.deltaTime;
            else
                speed = 20;
            foreach (var knife in lootKnives)
            {
                Vector2 direction = ((Vector2) transform.position - (Vector2) knife.position).normalized;
                knife.position += (Vector3) direction * speed * Time.deltaTime;
            }
            yield return null;
        }
        Controls.freezedControls--;
    }

    public void CollectAllknives()
    {
        foreach (var knife in lootKnives)
        {
            knife.position = transform.position;
        }
    }
}
