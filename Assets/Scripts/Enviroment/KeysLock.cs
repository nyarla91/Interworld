using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeysLock : Lock, IInteractable
{
    [SerializeField] private TextMeshPro _textMeshPro;
    public Sprite keysType;
    public int keysRequired;

    protected override void Awake()
    {
        base.Awake();
        _interactableClass = this;
        _textMeshPro.text = $"{keysRequired}";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == (int) Layer.Player)
            _textMeshPro.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == (int) Layer.Player)
            _textMeshPro.enabled = false;
    }

    public void Interact()
    {
        if (Saves.Data.keys.FindAll(t => t.Equals(keysType)).Count >= keysRequired)
        {
            Saves.Data.locksOpen.Add(name);
            Corpse newCorpse = Instantiate(_corpsePrefab, transform.position, Quaternion.identity).GetComponent<Corpse>();
            newCorpse.Init(spriteRenderer.sprite, false, 1);
            if (OnUnlock != null)
                OnUnlock();
            Saves.Save();
            Destroy(gameObject);
        }
    }
}
