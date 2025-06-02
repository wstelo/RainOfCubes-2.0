using UnityEngine;

[RequireComponent (typeof(Exploder))]

public class BombSpawner : Spawner<Bomb>
{
    private Exploder _exploder;
    private Vector3 _spawnPosition;

    public override void Awake()
    {
        _exploder = GetComponent<Exploder>();
        base.Awake();
    }

    public void GetBomb(Vector3 position)
    {
        _spawnPosition = position;
        Pool.Get();
    }

    public override void ReleasedObject(Item item)
    {       
        base.ReleasedObject(item);
        _exploder.Explode(item); 
        item.RefreshParameters();
        Pool.Release(item);
    }

    public override void GetObject(Item cube)
    {
        base.GetObject(cube);
        cube.transform.position = _spawnPosition;
        cube.transform.rotation = Quaternion.identity;
        cube.gameObject.SetActive(true);
    }
}
