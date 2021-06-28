using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteract : Transformer
{
    public static List<Interactable> Interactables = new List<Interactable>();

    private void Update()
    {
        if (Interactables.Count > 1)
        {
            Interactables.OrderBy(t => Vector2.Distance(transform.position, t.transform.position));
            AttachTooltip();
        }
        if (Controls.freezedControls <= 0 && Input.GetKeyDown(Controls.instance.keyInteract) && Interactables.Count > 0)
        {
            Interactables[0].Interacted();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Interactable otherInteractable = other.gameObject.GetComponent<Interactable>();
        if (otherInteractable != null)
        {
            Interactables.Add(otherInteractable);
            AttachTooltip();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable otherInteractable = other.gameObject.GetComponent<Interactable>();
        if (otherInteractable != null && Interactables.Contains(otherInteractable))
        {
            Interactables.Remove(otherInteractable);
            AttachTooltip();
        }
    }

    public void AttachTooltip()
    {
        if (Interactables.Count > 0)
            InteractableTooltip.instance.MoveTo(Interactables[0]);
        else
            InteractableTooltip.instance.Hide();
    }
}
