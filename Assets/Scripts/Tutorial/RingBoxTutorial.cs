using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBoxTutorial : TriggerTutorial
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private RingBox _ringBox;

    protected override void Awake()
    {
        base.Awake();
        _ringBox.OnOpen += Launch;
    }

    protected override void Launch()
    {
        if (Saves.Data.rings.Count > 0)
        {
            _tutorialLabel = new[]
            {
                "Rings activation",
                "Активация колец"
            };
            _tutorialDescription = new[]
            {
                "Each ring has its power. The more sanity you have the more powerful ring you may wear\n." +
                "Effect of the ring is triggered when you take damage in combat. You also may press [F] and spend 2 energy to" +
                "use the rings without getting damage",
                "Каждое кольцо имеет свою силу. Чем больше у вас рассудка, тем более могущественные кольца вы сможете носить.\n" +
                "Эффект кольца срабатывает каждый раз, когда вы получаете урон в бою. Также вы можете нажать [F] и потратить 2" +
                " энергии, чтобы активировать кольца без получения урона."
            };
            _icon = _spriteRenderer.sprite;
        }
        base.Launch();
    }
}
