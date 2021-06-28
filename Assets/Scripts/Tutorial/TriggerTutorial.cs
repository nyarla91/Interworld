using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTutorial : Transformer
{
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected string[] _tutorialLabel;
    [SerializeField] [TextArea(5, 10)] protected string[] _tutorialDescription;
    private void Awake()
    {
        if (Saves.Data.tutorialsComplete.Contains(name))
            Destroy(this);
    }

    protected virtual void Launch()
    {
        if (!Saves.Data.tutorialsComplete.Contains(name))
        {
            Tutorial.instance.ShowTutorial(_icon, _tutorialLabel[Localization.language], _tutorialDescription[Localization.language]);
            Saves.Data.tutorialsComplete.Add(name);
            Destroy(this);
        }
    }
}
