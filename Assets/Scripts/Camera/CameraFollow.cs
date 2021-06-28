using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Transformer
{
    public static CameraFollow instance;

    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    public Vector2 anchor;
    public float interpolation;
    public float size;

    public bool freezed;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
        transform.position = _target.position;
    }

    private void FixedUpdate()
    {
        if (!freezed)
        {
            Vector2 targetPoint = Vector2.Lerp(_target.position, anchor, 1 - interpolation);
            transform.position = VectorHelper.SetZ(Vector2.Lerp(transform.position, targetPoint, _speed), -100);
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, size, _speed);
        }
    }
    

    public void ApplySettings(Vector2 anchor, float interpolation, float size)
    {
        this.anchor = anchor;
        this.interpolation = interpolation;
        this.size = size;
    }

    public void Reset()
    {
        transform.position = _target.position;
        _camera.orthographicSize = size;
    }
}
