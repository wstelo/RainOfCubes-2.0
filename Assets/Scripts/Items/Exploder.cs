using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private int _explosionForce = 200;
    [SerializeField] private int _explosionRadius = 10;

    public void Explode(Item cube)
    {
        foreach (var explodableObject in GetExplodableObjects(cube.transform))
        {
            explodableObject.AddExplosionForce(_explosionForce, cube.transform.position, _explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObjects(Transform cube)
    {
        Collider[] hits = Physics.OverlapSphere(cube.position, _explosionRadius);
        List<Rigidbody> cubes = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }

        return cubes;
    }
}
