using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeleportInList : Transformer
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private TeleportPoint _point;

    public void Init(TeleportPoint point)
    {
        _point = point;
        _textMeshPro.text = _point.teleportName;
    }

    public void Teleport()
    {
        TeleportList.instance.Toggle(false);
        Saves.DiscardLocalSaves();
        Saves.Save();
        Locations.LoadLocation(_point.location, _point.exit, SaveType.Global);
    }
}
