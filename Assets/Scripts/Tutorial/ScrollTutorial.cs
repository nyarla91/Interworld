using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTutorial : TriggerTutorial
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Scroll _scroll;
    
    private string[] _keptDescription;

    protected override void Awake()
    {
        base.Awake();
        _keptDescription = _tutorialDescription;
        _scroll.OnCollected += Launch;
    }

    protected override void Launch()
    {
        if (Saves.Data.scrolls.FindAll(t => t.Equals(_spriteRenderer.sprite)).Count < Scroll.SCROLLS_REQUIRED)
        {
            _tutorialDescription = new[]
            {
                "Collect another similar scroll to gain some upgrade or ability.",
                "Соберите ещё один такой же свиток чтобы получить улучшение или способность."
            };
        }
        else
        {
            _tutorialDescription = _keptDescription;
        }
        base.Launch();
    }
}
