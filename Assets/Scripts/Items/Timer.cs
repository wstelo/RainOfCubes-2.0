using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _timeToDestroy;

    public event Action EndedTime;
    public event Action<float,float> ChangedTime;

    public void Enable(float timeToDestroy)
    {
        _timeToDestroy = timeToDestroy;
        StartCoroutine(TimeCounting());
    }
    
    private IEnumerator TimeCounting()
    {
        float currentTime = 0;
        float secondsCount = 1;
        var wait = new WaitForSeconds(secondsCount);

        ChangedTime?.Invoke(currentTime, _timeToDestroy);

        while (currentTime < _timeToDestroy)
        {
            yield return wait;
            currentTime++;
            ChangedTime?.Invoke(currentTime, _timeToDestroy);
        }

        EndedTime?.Invoke();
    }
}
