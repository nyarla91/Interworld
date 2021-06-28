using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSpikes : Spikes
{
    [SerializeField] private float _delay;
    [SerializeField] private float _duration;

    private Coroutine raiseCorutine;

    private float alpha
    {
        get => _spriteRenderer.color.a;
        set => _spriteRenderer.color = VectorHelper.SetA(_spriteRenderer.color, value);
    }

    private void Start()
    {
        alpha = 0.7f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == (int) Layer.Player && raiseCorutine == null)
        {
            raiseCorutine = StartCoroutine(Raise());
        }
    }

    private IEnumerator Raise()
    {
        alpha = 1;
        yield return new WaitForSeconds(_delay);
        raised = true;
        yield return new WaitForSeconds(_duration);
        raised = false;
        alpha = 0.7f;
        raiseCorutine = null;
    }
}
