using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlTutorial : TriggerTutorial
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == (int) Layer.PlayerProjectile)
        {
            Launch();
        }
    }
}
