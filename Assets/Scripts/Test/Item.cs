using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody), typeof(Timer))]

public abstract class Item : MonoBehaviour
{
    [SerializeField] private Timer _timer;

    public event Action<Item> ObjectIdled;

    private void OnEnable()
    {
        _timer.EndedTime += DisableObject;
    }

    private void OnDisable()
    {
        _timer.EndedTime -= DisableObject;
    }

    private void DisableObject()
    {
        ObjectIdled?.Invoke(this);
    }

    public abstract void RefreshParameters();
}
