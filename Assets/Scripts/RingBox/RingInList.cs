using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RingInList : Transformer
{
    [SerializeField] private Image _panel;
    [SerializeField] private Sprite _activePanel;
    [SerializeField] private Sprite _inactivePanel;
    [SerializeField] private Image _ringImage;
    [SerializeField] private TextMeshProUGUI _nameTextMeshPro;
    [SerializeField] private TextMeshProUGUI _descriptionTextMeshPro;
    [SerializeField] private TextMeshProUGUI _powerTextMeshPro;

    private RingInfo _myRing;

    public void Init(RingInfo ring)
    {
        _myRing = ring;
        _ringImage.sprite = _myRing.type;
        _nameTextMeshPro.text = _myRing.ringName[Localization.language];
        _descriptionTextMeshPro.text = $"{Localization.Translate("<rinEffect>")}{_myRing.description[Localization.language]}";
        _powerTextMeshPro.text = _myRing.power.ToString();
        if (Saves.Data.ringsActive.Contains(_myRing.type))
            _panel.sprite = _activePanel;
    }

    public void Click()
    {
        if (Saves.Data.ringsActive.Contains(_myRing.type))
        {
            Saves.Data.ringsActive.Remove(_myRing.type);
            _panel.sprite = _inactivePanel;
            Saves.Data.sanityUsed -= _myRing.power;
        }
        else if (Saves.Data.sanityUsed + _myRing.power <= Saves.Data.sanity)
        {
            Saves.Data.ringsActive.Add(_myRing.type);
            _panel.sprite = _activePanel;
            Saves.Data.sanityUsed += _myRing.power;
        }
        RingsList.instance.UpdateSanity();
    }
}
