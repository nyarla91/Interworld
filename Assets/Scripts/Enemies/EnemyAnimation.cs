using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private string namePrefix;
    private string _currentState;

    public Color tint
    {
        set => _spriteRenderer.color = VectorHelper.SetA(value, _spriteRenderer.color.a);
    }

    public float alpha
    {
        set => _spriteRenderer.color = VectorHelper.SetA(_spriteRenderer.color, value);
    }

    public Color color => _spriteRenderer.color;

    public void PlayState(string state)
    {
        state = namePrefix + state;
        if (_currentState != state)
        {
            _animator.Play(state);
            _currentState = state;
        }
    }
}
