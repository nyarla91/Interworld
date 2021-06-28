using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PlayerMovement : Transformer
{
    [SerializeField] private Rigidbody2D _rigidbody;
    public float speed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashCooldown;

    public delegate void DashHander(Vector2 direction);
    public DashHander OnDash;
    public delegate void DashEndHander();
    public DashEndHander OnDashEnd;

    private Vector2 _direction;
    private Dictionary<string, float> _speedModifiers;
    private bool _dashReady = true;
    private bool _dashing;
    private Coroutine _dashCoroutine;

    public float countedSpeed
    {
        get
        {
            float returned = speed;
            if (_speedModifiers.Count > 0)
                foreach (var modifier in _speedModifiers)
                {
                    returned *= modifier.Value;
                }
            return returned;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _speedModifiers = new Dictionary<string, float>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (_dashReady && Saves.Data.dash && Controls.freezedControls <= 0 && Input.GetKeyDown(Controls.instance.keyDash))
            _dashCoroutine = StartCoroutine(Dash(_dashSpeed, _dashDuration, _dashCooldown));
    }

    private void Move()
    {
        _direction = Vector2.Lerp(_direction, Controls.moveAxis, _acceleration);
        if (!_dashing)
            _rigidbody.velocity = _direction * countedSpeed;
    }

    private IEnumerator Dash(float speed, float duration, float cooldown)
    {
        _dashReady = false;
        _dashing = true;
        Controls.freezedControls++;
        Vector2 direction = Controls.DirectionFromPosition(transform.position);
        if (OnDash != null)
            OnDash(direction);
        Sound.Play(Player.instance.sound.dashSound);
        for (int i = 1; i >= 0; i--)
        {
            float time = 0;
            float halfDuration = duration / 2;
            while (time < halfDuration)
            {
                float koff = Time.deltaTime;
                if (time + koff > halfDuration)
                    koff = halfDuration - time;
                _rigidbody.velocity = Mathf.Lerp(_rigidbody.velocity.magnitude, _dashSpeed * i, koff * 6) * direction;
                time += koff;
                yield return new WaitForSeconds(koff);
            }
        }
        _dashing = false;
        if (OnDashEnd != null)
            OnDashEnd();
        _rigidbody.velocity = Vector2.zero;
        Controls.freezedControls--;
        yield return new WaitForSeconds(cooldown);
        _dashReady = true;
    }

    public void AddSpeedModifier(string label, float modifier)
    {
        if (!_speedModifiers.ContainsKey(label))
            _speedModifiers.Add(label, modifier);
    }

    public void CancelSpeedModifier(string label)
    {
        if (_speedModifiers.ContainsKey(label))
        {
            _speedModifiers.Remove(label);
        }
    }

    public void InterruptDash()
    {
        if (_dashing)
        {
            StopCoroutine(_dashCoroutine);
            _dashing = false;
            if (OnDashEnd != null)
                OnDashEnd();
            _rigidbody.velocity = Vector2.zero;
            Controls.freezedControls--;
            _dashReady = true;
        }
    }

    public void ResetSpeed()
    {
        _speedModifiers = new Dictionary<string, float>();
    }
}
