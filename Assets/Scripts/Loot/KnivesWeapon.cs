using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnivesWeapon : Loot, ILoot
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string[] _tutorialLabel;
    [SerializeField] [TextArea(5, 10)] private string[] _tutorialDescription;
    
    protected override void Awake()
    {
        base.Awake();
        lootClass = this;
    }

    public void Collect()
    {
        Saves.Data.knives = true;
        Tutorial.instance.ShowTutorial(_sprite, _tutorialLabel[Localization.language], _tutorialDescription[Localization.language]);
    }
}
