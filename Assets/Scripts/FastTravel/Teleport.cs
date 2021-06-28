using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Transformer
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private string[] _teleportName;
    [SerializeField] private int _exit;
    [SerializeField] private float _priority;
    private TeleportPoint point;
    
    protected override void Awake()
    {
        base.Awake();
        point = new TeleportPoint(Locations.currentLocationNumber, _exit, _teleportName[Localization.language], _priority);
        if (!Saves.Data.teleportNames.Contains(point.teleportName))
            _spriteRenderer .color = Color.gray;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == (int) Layer.Player)
            Unlock();
    }

    private void Unlock()
    {
        if (!Saves.Data.teleportNames.Contains(point.teleportName))
        {
            _spriteRenderer .color = Color.white;
            Saves.Data.teleportPointsOpen.Add(point);
            Saves.Data.teleportNames.Add(point.teleportName);
        }
    }
}

[Serializable]
public class TeleportPoint
{
    public int location;
    public int exit;
    public string teleportName;
    public float priority;

    public TeleportPoint(int location, int exit, string teleportName, float priority)
    {
        this.location = location;
        this.exit = exit;
        this.teleportName = teleportName;
        this.priority = priority;
    }
}