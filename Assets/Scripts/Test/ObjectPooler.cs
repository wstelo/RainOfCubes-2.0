//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Pool;
//using UnityEngine.UIElements;

//public class ObjectPooler <T> : MonoBehaviour where T : Item
//{
//    [SerializeField] private T _prefab;
//    [SerializeField] private int _poolCapacity = 5;

//    private ObjectPool<Item> _pool;
//    private int _poolMaxSize = 10;

//    private void Awake()
//    {
//        _pool = new ObjectPool<Item>(
//            createFunc: () => CreateObject(),
//            actionOnGet: (cube) => GetObject(cube),
//            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
//            defaultCapacity: _poolCapacity,
//            actionOnDestroy: (cube) => DestroyObject(cube),
//            maxSize: _poolMaxSize);
//    }

//    private Item CreateObject()
//    {
//        Item cube = Instantiate(_prefab, gameObject.transform.position, Quaternion.identity);
//        cube.Timer.EndedTime += ReleasedObject;
        
//        return cube;
//    }

//    private void DestroyObject(Item cube)
//    {
//        cube.Timer.EndedTime -= ReleasedObject;
//        Destroy(cube.gameObject);
//    }

//    private void GetObject(Item cube)
//    {
//        //Vector3 position = new Vector3(Random.Range(_minPositionX, _maxPositionX), _positionY, Random.Range(_minPositionZ, _maxPositionZ));
//        //cube.transform.position = position;
//        //cube.transform.rotation = Quaternion.identity;
//        cube.gameObject.SetActive(true);
//    }

//    private void ReleasedObject(Item cube)
//    {
//        cube.RefreshParameters();
//        _pool.Release(cube);
//    }
//}
