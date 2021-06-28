using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool _infinite;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((_infinite || !Saves.QuickData.checkpointsUsed.Contains(name)) && other.gameObject.layer == (int) Layer.Player)
        {
            Saves.QuickData.checkpointsUsed.Add(name);
            Saves.Save();
        }
    }
}
