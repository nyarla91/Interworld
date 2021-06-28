using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _doors = new Sprite[2];

    private void Awake()
    {
        _spriteRenderer.sprite = _doors[0];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            SwitchDoor(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            SwitchDoor(false);
        }
    }

    private void SwitchDoor(bool opened)
    {
        _collider.enabled = !opened;
        _spriteRenderer.sprite = _doors[BoolHelper.BoolToInt(opened)];
    }
}
