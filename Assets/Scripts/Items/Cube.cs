using UnityEngine;
using System;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody),typeof(Timer))]
[RequireComponent (typeof(CubeCollisionDetector))]

public class Cube : Item
{
    private CubeCollisionDetector _detector;
    private Rigidbody _rigidbody;
    private Material _material;
    private int _minDelay = 2;
    private int _maxDelay = 5;
    private Color _defaultColor;

    public event Action CollisionDetected;

    public override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
        _material = GetComponent<MeshRenderer>().material;
        _detector = GetComponent<CubeCollisionDetector>();
        DelayToDestroy = UnityEngine.Random.Range(_minDelay, _maxDelay + 1);
        _defaultColor = _material.color;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        _detector.RequiredSurfaceDetected += Encounter;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _detector.RequiredSurfaceDetected -= Encounter;
    }

    private void Encounter()
    {
        SetRandomColor();
        CollisionDetected?.Invoke();
        Timer.Enable(DelayToDestroy);
    }

    public override void RefreshParameters()
    {
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.velocity = Vector3.zero;
        DelayToDestroy = UnityEngine.Random.Range(_minDelay, _maxDelay + 1);
        _material.color = _defaultColor;
        _detector.Refresh();
    }

    private void SetRandomColor()
    {
        _material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 255);
    }
}
