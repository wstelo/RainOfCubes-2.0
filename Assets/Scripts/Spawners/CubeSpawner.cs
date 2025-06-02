using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private InputService _inputService;
    [SerializeField] private int _minPositionX;
    [SerializeField] private int _maxPositionX;
    [SerializeField] private int _minPositionZ;
    [SerializeField] private int _maxPositionZ;
    [SerializeField] private int _positionY;

    private int _poolDelay = 2;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        _currentCoroutine = StartCoroutine(EnableObject());
    }

    private void OnEnable()
    {
        _inputService.StopButtonClicked += StopSpawn;
    }

    private void OnDisable()
    {
        _inputService.StopButtonClicked -= StopSpawn;
    }

    private void StopSpawn()
    {
        StopCoroutine(_currentCoroutine);
    }

    private IEnumerator EnableObject()
    {
        while (enabled)
        {
            var wait = new WaitForSeconds(_poolDelay);
            yield return wait;
            Pool.Get();
        }
    }

    public override void ReleasedObject(Item item)
    {
        base.ReleasedObject(item);
        _bombSpawner.EnableObject(item.transform.position);
        item.RefreshParameters();
        Pool.Release(item);
    }

    public override void Initialize(Item cube)
    {
        base.Initialize(cube);
        Vector3 position = new Vector3(Random.Range(_minPositionX, _maxPositionX), _positionY, Random.Range(_minPositionZ, _maxPositionZ));
        cube.transform.position = position;
        cube.transform.rotation = Quaternion.identity;
        cube.gameObject.SetActive(true);
    }
















}
