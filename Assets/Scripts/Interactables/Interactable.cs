using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : Transformer
{
    protected IInteractable _interactableClass;

    public void Interacted()
    {
        if (_interactableClass != null)
            _interactableClass.Interact();
    }
}
