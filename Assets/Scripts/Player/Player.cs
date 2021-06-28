using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Transformer
{
    public static Player instance;

    public PlayerWeapon weapon;
    public PlayerMovement movement;
    public PlayerUI ui;
    public PlayerAnimation animation;
    public PlayerStatus status;
    public PlayerSound sound;
    public PlayerRings rings;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public void Reset()
    {
        movement.InterruptDash();
        rings.StopAllCoroutines();
        movement.ResetSpeed();
        weapon.StopAiming();
        weapon.CollectAllknives();
    }
}
