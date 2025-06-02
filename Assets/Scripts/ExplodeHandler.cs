using UnityEngine;

[RequireComponent(typeof(Exploder))]
public class ExplodeHandler : MonoBehaviour
{
    [SerializeField] private BombSpawner _spawner;

    private Exploder _exploder;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
    }

    private void OnEnable()
    {
        _spawner.ObjectReleased += ExplodeObject;
    }

    private void OnDisable()
    {
        _spawner.ObjectReleased -= ExplodeObject;
    }

    private void ExplodeObject(Item item)
    {
        _exploder.Explode(item);
    }
}
