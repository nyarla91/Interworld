using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PeriodSpikeGroup : Transformer
{
    public float raisedTime;
    public float fallenTime;
    public float degreeOffset;

    private List<PeriodSpikes> _group;

    private void Awake()
    {
        _group = transform.GetComponentsInChildren<PeriodSpikes>().ToList();
        foreach (var spike in _group)
        {
            spike.SetGroupProperties(this);
        }
    }
}
