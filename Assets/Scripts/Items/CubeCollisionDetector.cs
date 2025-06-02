using System;
using UnityEngine;

public class CubeCollisionDetector : MonoBehaviour
{
    private bool _hasFirstCollision = false;

    public event Action RequiredSurfaceDetected;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform) && _hasFirstCollision == false)
        {
            _hasFirstCollision = true;
            RequiredSurfaceDetected?.Invoke();
        }
    }

    public void Refresh()
    {
        _hasFirstCollision = false;
    }

}
