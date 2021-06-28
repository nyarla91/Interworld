using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    [SerializeField] private Animator _animator;
    private string _currentState = "";
    private bool _verticalPriority;
    private Dictionary<Vector2, string> _walkClips;
    private bool _dashing;

    private void Awake()
    {
        Player.instance.movement.OnDash += Dash;
        Player.instance.movement.OnDashEnd += () => { _dashing = false; };
    }

    public void PlayState(string state)
    {
        state = "Player" + state;
        if (state == _currentState)
            return;
        _animator.Play(_currentState = state);
    }

    private void Update()
    {
        if (Input.GetKeyDown(Controls.instance.keyMoveLeft) || Input.GetKeyDown(Controls.instance.keyMoveRight))
            _verticalPriority = true;
        if (Input.GetKeyDown(Controls.instance.keyMoveDown) || Input.GetKeyDown(Controls.instance.keyMoveUp))
            _verticalPriority = false;
        _animator.speed = Player.instance.movement.countedSpeed / Player.instance.movement.speed;
        PlayWalkAnimations();
    }

    private void PlayWalkAnimations()
    {
        if (!_dashing)
        {
            if (Controls.moveAxis.magnitude == 0)
            {
                string[] directions = _currentState.Split(new[] {'_'});
                if (directions.Length > 1)
                    PlayState($"{Animations.IDLE}_{directions[2]}");
                return;
            }

            if (Controls.moveAxis.x > 0)
            {
                PlayState(Animations.WALK + Animations.RIGHT);
                return;
            }

            if (Controls.moveAxis.x < 0)
            {
                PlayState(Animations.WALK + Animations.LEFT);
                return;
            }
            if (Controls.moveAxis.y > 0)
                PlayState(Animations.WALK + Animations.UP);
            if (Controls.moveAxis.y < 0)
                PlayState(Animations.WALK + Animations.DOWN);
        }
    }

    private void Dash(Vector2 direction)
    {
        _dashing = true;
        if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))
        {
            direction.y = 0;
            MathHelper.Normalize(ref direction.x);
        }
        else
        {
            direction.x = 0;
            MathHelper.Normalize(ref direction.y);
        }
        
        if (direction.Equals(Vector2.left))
            PlayState(Animations.DASH + Animations.LEFT);
        else if (direction.Equals(Vector2.right))
            PlayState(Animations.DASH + Animations.RIGHT);
        else if (direction.Equals(Vector2.up))
            PlayState(Animations.DASH + Animations.UP);
        else if (direction.Equals(Vector2.down))
            PlayState(Animations.DASH + Animations.DOWN);
    }

    public IEnumerator DamageBlinking()
    {
        while (true)
        {
            float alpha = 0.5f + 0.5f * (Mathf.Sin(Time.time * 24) + 1) / 2;
            spriteRenderer.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }
}
