using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBox : Interactable, IInteractable
{
    public Scroll.CollectedHandler OnOpen;
    
    protected override void Awake()
    {
        base.Awake();
        _interactableClass = this;
    }

    public void Interact()
    {
        RingsList.instance.Toggle(true);
        if (OnOpen != null)
            OnOpen();
    }
}
