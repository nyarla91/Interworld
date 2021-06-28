using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnifeAiming : Transformer
{
    private const float Z_POSITION_OFFSET = -10;

    private PlayerWeapon _player;
    private float _range;

    public void Init(PlayerWeapon player, float range)
    {
        _player = player;
        _range = range;
    }
    
    private void Update()
    {
        Vector2 direction = Controls.DirectionFromPosition(_player.transform.position);
        transform.position = _player.transform.position + (Vector3) direction * _range * _player.impulsePercent + Vector3.forward * Z_POSITION_OFFSET;
        transform.rotation = Quaternion.Euler(0, 0, VectorHelper.Vector2ToDegrees(direction) + PlayerKnife.Z_ROTATION_OFFSET);
    }
}
