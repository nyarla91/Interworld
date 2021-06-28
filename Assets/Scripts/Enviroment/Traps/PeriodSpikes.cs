using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodSpikes : Spikes
{
    [SerializeField] private float raisedTime;
    [SerializeField] private float fallenTime;
    [SerializeField] private float offset;
    [SerializeField] private int degree;

    private void Start()
    {
        StartCoroutine(RaiseFall());
    }

    private IEnumerator RaiseFall()
    {
        yield return new WaitForSeconds(offset);
        while (true)
        {
            raised = true;
            yield return new WaitForSeconds(raisedTime);
            raised = false;
            yield return new WaitForSeconds(fallenTime);
        }
    }

    public void SetGroupProperties(PeriodSpikeGroup group)
    {
        raisedTime = group.raisedTime;
        fallenTime = group.fallenTime;
        offset = group.degreeOffset * degree;
    }
    
}