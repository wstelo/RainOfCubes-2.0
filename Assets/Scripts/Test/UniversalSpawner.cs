using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Progress;

public abstract class UniversalSpawner<T> : MonoBehaviour where T : Item
{
    [SerializeField] private T _prefab;
    [SerializeField] private InputService _inputService;
    [SerializeField] private int _minPositionX;
    [SerializeField] private int _maxPositionX;
    [SerializeField] private int _minPositionZ;
    [SerializeField] private int _maxPositionZ;
    [SerializeField] private int _positionY;
    [SerializeField] private int _poolCapacity = 5;

    private ObjectPool<Item> _pool;
    private Vector3 _position = new Vector3(1, 20, 3);
    private int _poolMaxSize = 10;
    private int _poolDelay = 2;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _pool = new ObjectPool<Item>(
            createFunc: () => CreateObject(),
            actionOnGet: (cube) => GetObject(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            defaultCapacity: _poolCapacity,
            actionOnDestroy: (cube) => DestroyObject(cube),
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        _currentCoroutine = StartCoroutine(GetCubes());
    }

    private void OnEnable()
    {
        _inputService.ButtonClicked += StopSpawn;
    }

    private void OnDisable()
    {
        _inputService.ButtonClicked -= StopSpawn;
    }

    private void StopSpawn()
    {
        StopCoroutine(_currentCoroutine);
    }

    private IEnumerator GetCubes()
    {
        while (enabled)
        {
            var wait = new WaitForSeconds(_poolDelay);
            yield return wait;
            _pool.Get();
        }
    }

    private Item CreateObject()
    {
        Item cube = Instantiate(_prefab, _position, Quaternion.identity);
        cube.ObjectIdled += ReleasedObject;

        return cube;
    }

    private void DestroyObject(Item cube)
    {
        cube.ObjectIdled -= ReleasedObject;
        Destroy(cube.gameObject);
    }

    private void GetObject(Item cube)
    {
        Vector3 position = new Vector3(Random.Range(_minPositionX, _maxPositionX), _positionY, Random.Range(_minPositionZ, _maxPositionZ));
        cube.transform.position = position;
        cube.transform.rotation = Quaternion.identity;
        cube.gameObject.SetActive(true);
    }

    private void ReleasedObject(Item cube)
    {
        cube.RefreshParameters();
        _pool.Release(cube);
    }
}
