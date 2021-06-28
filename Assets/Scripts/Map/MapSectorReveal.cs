using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSectorReveal : MonoBehaviour
{
    [SerializeField] private MapSector _sector;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == (int) Layer.Player && !_sector.revaled)
        {
            _sector.Reveal();
        }
    }
}
