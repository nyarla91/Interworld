using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class TeleportList : MonoBehaviour
{
    public static TeleportList instance;

    [SerializeField] private GameObject _teleportPrefab;
    [SerializeField] private RectTransform _content;
    [SerializeField] private CanvasGroup _canvasGroup;

    private List<TeleportInList> _teleports = new List<TeleportInList>();

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if (Input.GetKeyDown(Controls.instance.keyTeleport) && Saves.Data.teleportPointsOpen.Count > 0)
            instance.Toggle(true);
    }

    public void Toggle(bool open)
    {
        if (open && UIWindows.windowOpen)
            return;
        UIWindows.windowOpen = open;
        _canvasGroup.blocksRaycasts = open;
        _canvasGroup.alpha = BoolHelper.BoolToInt(open);
        _content.sizeDelta = new Vector2(1, 100 + Saves.Data.teleportPointsOpen.Count * 50);
        Saves.Data.teleportPointsOpen = Saves.Data.teleportPointsOpen.OrderBy(t => -t.priority).ToList();
        int i = 0;
        if (open)
            foreach (var teleport in Saves.Data.teleportPointsOpen)
            {
                TeleportInList newTeleport = Instantiate(_teleportPrefab, Vector3.zero, Quaternion.identity).GetComponent<TeleportInList>();
                newTeleport.rectTransform.SetParent(_content);
                newTeleport.rectTransform.anchoredPosition = new Vector2(50, -50 * (i + 1));
                newTeleport.Init(teleport);
                _teleports.Add(newTeleport);
                i++;
            }
        else
        {
            foreach (var teleport in _teleports)
            {
                Destroy(teleport.gameObject);
            }
            _teleports = new List<TeleportInList>();
        }
    }
}
