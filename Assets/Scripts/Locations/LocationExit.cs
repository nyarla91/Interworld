using System;
using UnityEngine;

public class LocationExit : Transformer
{
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected bool _enter = true;
    [SerializeField] protected bool _arena;
    [SerializeField] protected int location;
    [SerializeField] protected int exit;
    
    public enum Side
    {
        Left = 0,
        Right = 1,
        Up = 2,
        Down = 3,
    }
    [SerializeField] private Side _idleSide;
    public string idleSide
    {
        get
        {
            switch ((int) _idleSide)
            {
                case 0:
                    return Animations.LEFT;
                case 1:
                    return Animations.RIGHT;
                case 2:
                    return Animations.UP;
                default:
                    return Animations.DOWN;
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if (_arena)
        {
            if (Saves.Data.arenasCleared.Contains(name))
            {
                _enter = false;
                _spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            }
        }
        else if (_spriteRenderer != null)
            Destroy(_spriteRenderer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_enter && other.gameObject.GetComponent<Player>() != null)
        {
            if (_arena)
                Arena.current = name;
            Locations.LoadLocation(location, exit, SaveType.Local);
        }
    }
}
