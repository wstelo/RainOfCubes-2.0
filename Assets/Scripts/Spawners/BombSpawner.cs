using System;
using UnityEngine;


public class BombSpawner : Spawner<Bomb>
{

    public event Action <Item> ObjectReleased;

    public override void Awake()
    {
        base.Awake();
    }

    public void EnableObject(Vector3 position)
    {
        Item currentObject = Pool.Get();
        currentObject.transform.position = position;
    }

    public override void ReleasedObject(Item item)
    {       
        base.ReleasedObject(item);
        ObjectReleased?.Invoke(item);
        item.RefreshParameters();
        Pool.Release(item);
    }

    public override void Initialize(Item cube)
    {
        base.Initialize(cube);
        cube.transform.rotation = Quaternion.identity;
        cube.gameObject.SetActive(true);
    }
}
