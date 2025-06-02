using UnityEngine;
using System;

public class InputService : MonoBehaviour
{
    private const KeyCode _escapeKeyCode = KeyCode.Escape;

    public event Action StopButtonClicked;

    private void Update()
    {
        if (Input.GetKeyDown(_escapeKeyCode))
        {
            StopButtonClicked?.Invoke();
        }
    }
}
