using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGhost : Transformer
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private string _appearTrigger;
    [SerializeField] private string _disappearTrigger;
    [SerializeField] private string _turnToKingTrigger;
    [SerializeField] private Sprite _kingSprite;

    private float alpha
    {
        get => _spriteRenderer.color.a;
        set => _spriteRenderer.color = VectorHelper.SetA(_spriteRenderer.color, value);
    }

    private void Awake()
    {
        alpha = 0;
        Events.OnEvent += Appear;
        Events.OnEvent += Disappear;
        Events.OnEvent += TurnToKing;
        if (Saves.Data.bossKilled)
        {
            alpha = 1;
            _spriteRenderer.sprite = _kingSprite;
            transform.position = VectorHelper.SetZ(Saves.Data.bossDeathPosition, transform.position.z);
        }
    }

    private void Appear(string type)
    {
        if (type.Equals(_appearTrigger))
            StartCoroutine(Fade(true));
    }

    private void Disappear(string type)
    {
        if (type.Equals(_disappearTrigger))
            StartCoroutine(Fade(false));
    }

    private IEnumerator Fade(bool fadeIn)
    {
        float targetAlpha = BoolHelper.BoolToInt(fadeIn) * 0.9f;
        while (Mathf.Abs(alpha - targetAlpha) > 0.01f)
        {
            alpha = Mathf.Lerp(alpha, targetAlpha, Time.deltaTime * 3);
            yield return null;
        }
        alpha = targetAlpha;
    }

    private void TurnToKing(string type)
    {
        if (type.Equals(_turnToKingTrigger))
        {
            StartCoroutine(TurnToKingIE());
        }
    }

    private IEnumerator TurnToKingIE()
    {
        alpha = 1;
        _spriteRenderer.sprite = _kingSprite;
        Vector3 targetPosition = VectorHelper.SetZ(Saves.Data.bossDeathPosition, transform.position.z);
        while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * 2);
            yield return null;
        }
        transform.position = targetPosition;
    }

    private void OnDestroy()
    {
        Events.OnEvent -= Appear;
        Events.OnEvent -= Disappear;
        Events.OnEvent -= TurnToKing;
    }
}
