using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSector : MonoBehaviour
{
    [SerializeField] private GameObject _sector;

    public bool revaled => _sector.activeSelf;

    private void Awake()
    {
        if (Saves.Data.mapSectorsOpen.Contains(name))
        {
            _sector.SetActive(true);
        }
        else
            _sector.SetActive(false);
    }

    public void Reveal()
    {
        Saves.Data.mapSectorsOpen.Add(name);
        _sector.SetActive(true);
    }
}
