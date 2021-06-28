using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : UIAlpha
{
    public static Tutorial instance;
    public static bool tutorial;
    
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _description;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public void ShowTutorial(Sprite icon, string label, string description)
    {
        if (Saves.test)
            return;
        Controls.freezedControls++;
        Show();
        _icon.sprite = icon;
        _label.text = label;
        _description.text = description;
    }

    public void Close()
    {
        Controls.freezedControls--;
        Hide();
        Saves.Save();
    }
}
