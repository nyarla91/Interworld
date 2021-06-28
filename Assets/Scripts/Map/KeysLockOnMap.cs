using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeysLockOnMap : MapObject
{
    [SerializeField] private TextMeshPro _textMeshPro;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private string[] type;
    [SerializeField] private KeysLock _reference;

    private bool _unlocked;

    private void Awake()
    {
        _reference.OnUnlock += Unlock;
        _textMeshPro.text = _reference.keysRequired.ToString();
    }

    protected override void OnMouseEnter()
    {
        description = $"{type[Localization.language]} {Localization.Translate("<mapLock>").ToLower()} ";
        if (!_unlocked)
            description += $"({Saves.Data.keys.FindAll(t => t.Equals(_reference.keysType)).Count}/{_reference.keysRequired})";
        else
            description += $"({Localization.Translate("<mapUnlocked>")})";
        base.OnMouseEnter();
    }

    private void Unlock()
    {
        _unlocked = true;
        _spriteRenderer.color = VectorHelper.SetA(_spriteRenderer.color, 0.5f);
        Destroy(_textMeshPro.gameObject);
    }
}
