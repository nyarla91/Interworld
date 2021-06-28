using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterTutorial : TriggerTutorial
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == (int) Layer.Player)
        {
            Launch();
        }
    }
}
