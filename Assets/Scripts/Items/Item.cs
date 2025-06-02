using System;
using UnityEngine;


public abstract class Item : MonoBehaviour
{
    protected Timer Timer;

    public event Action<Item> ObjectIdled;

    public int DelayToDestroy { get; protected set; }

    public virtual void Awake()
    {
        Timer = GetComponent<Timer>();
    }

    public virtual void OnEnable()
    {
        Timer.EndedTime += DisableObject;
    }

    public virtual void OnDisable()
    {
        Timer.EndedTime -= DisableObject;
    }

    private void DisableObject()
    {
        ObjectIdled?.Invoke(this);
    }

    public abstract void RefreshParameters();
}
