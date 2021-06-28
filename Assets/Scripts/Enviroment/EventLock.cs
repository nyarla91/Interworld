using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLock : Lock, IInteractable
{
    [SerializeField] private string _eventType;
    [SerializeField] private bool _permanent = true;
    
    protected override void Awake()
    {
        base.Awake();
        _interactableClass = this;
        Events.OnEvent += Open;
    }

    private void Open(string eventType)
    {
        if (!eventType.Equals(_eventType))
            return;
        if (_permanent)
        {
            Saves.Data.locksOpen.Add(name);
            Saves.Save();
        }
        Corpse newCorpse = Instantiate(_corpsePrefab, transform.position, Quaternion.identity).GetComponent<Corpse>();
        newCorpse.Init(spriteRenderer.sprite, false, 1);
        if (OnUnlock != null)
            OnUnlock();
        Destroy(gameObject);
    }

    public void Interact()
    {
        UIMessage.ShowMessage(Localization.Translate("<mesLocked>"));
    }

    private void OnDestroy()
    {
        Events.OnEvent -= Open;
    }
}
