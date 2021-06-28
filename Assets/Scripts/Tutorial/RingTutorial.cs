using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTutorial : TriggerTutorial
{
    [SerializeField] private Sprite _ringBox;
    [SerializeField] private Ring _ring;

    protected override void Awake()
    {
        base.Awake();
        _ring.OnCollected += Launch;
    }

    protected override void Launch()
    {
        if (Saves.Data.rings.Count == 0)
        {
            _tutorialLabel = new[]
            {
                "Rings",
                "Кольца"
            };
            _tutorialDescription = new[]
            {
                "You've found the first ring. Use a ring box to activate it.",
                "Вы нашли первое кольцо, чтобы активировать его используйте любую шкатулку."
            };
            _icon = _ringBox;
            base.Launch();
        }
    }
}
