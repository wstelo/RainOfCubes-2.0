using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Timer))]

public class Bomb : Item
{
    private Material _material;
    private Color _defaultColor;
    private int _minDelay = 2;
    private int _maxDelay = 5;

    public override void Awake()
    {
        base.Awake();
        _material = GetComponent<MeshRenderer>().material;
        DelayToDestroy = Random.Range(_minDelay, _maxDelay + 1);
        _defaultColor = _material.color;
    }

    private void Start()
    {
        Timer.Enable(DelayToDestroy);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        Timer.Enable(DelayToDestroy);
        Timer.ChangedTime += DecreaseTransparency;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        Timer.ChangedTime -= DecreaseTransparency;
    }

    public override void RefreshParameters()
    {
        DelayToDestroy = Random.Range(_minDelay, _maxDelay + 1);
        _material.color = _defaultColor;
    }

    private void DecreaseTransparency(float currentTime, float maxTime)
    {
        Color defaultColor = _material.color;
        defaultColor.a = (maxTime / maxTime) - (currentTime / maxTime);
        _material.color = defaultColor;
    }
}
