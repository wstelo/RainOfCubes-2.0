using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : Item
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 5;

    protected ObjectPool<Item> Pool;
    private Vector3 _position = new Vector3(1, 20, 3);
    private int _poolMaxSize = 10;
    private int _totalObjectCount = 0;
    private int _createdObjectCount = 0;
    private int _activeObjectCount = 0;

    public event Action <int> TotalCountChanged;
    public event Action <int> CreatedCountChanged;
    public event Action <int> ActiveCountChanged;

    public virtual void Awake()
    {
        Pool = new ObjectPool<Item>(
            createFunc: () => CreateObject(),
            actionOnGet: (item) => Initialize(item),
            actionOnRelease: (item) => item.gameObject.SetActive(false),
            defaultCapacity: _poolCapacity,
            actionOnDestroy: (item) => DestroyObject(item),
            maxSize: _poolMaxSize);
    }

    public virtual void Initialize(Item item)
    {
        _totalObjectCount++;
        _activeObjectCount++;
        TotalCountChanged?.Invoke(_totalObjectCount);
        ActiveCountChanged?.Invoke(_activeObjectCount);
    }

    public virtual void ReleasedObject(Item item)
    {
        _activeObjectCount--;
        ActiveCountChanged?.Invoke(_activeObjectCount);
    }

    private Item CreateObject()
    {
        Item item = Instantiate(_prefab, _position, Quaternion.identity);
        item.ObjectIdled += ReleasedObject;
        _createdObjectCount++;
        CreatedCountChanged?.Invoke(_createdObjectCount);

        return item;
    }

    private void DestroyObject(Item item)
    {
        item.ObjectIdled -= ReleasedObject;
        _createdObjectCount--;
        CreatedCountChanged?.Invoke(_createdObjectCount);
        Destroy(item.gameObject);
    }
}
