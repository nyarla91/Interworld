using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite raisedSprite;
    [SerializeField] private Sprite fallenSprite;
    [SerializeField] private int _damage;
    [SerializeField] private string _turnOffTrigger;

    private bool _raised;
    public bool raised
    {
        get => _raised;
        set
        {
            _raised = value;
            if (value)
                _spriteRenderer.sprite = raisedSprite;
            else
                _spriteRenderer.sprite = fallenSprite;
        }
    }

    private void Awake()
    {
        Events.OnEvent += TurnOff;
    }

    private void TurnOff(string eventType)
    {
        if (eventType.Equals(_turnOffTrigger))
        {
            print("Турнофф");
            StopAllCoroutines();
            raised = false;
            Destroy(this);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals((int) Layer.Player) && raised)
        {
            Player.instance.status.TakeDamage(_damage);
        }
    }

    private void OnDestroy()
    {
        Events.OnEvent -= TurnOff;
    }
}
