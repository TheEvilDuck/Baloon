using UnityEngine;
using System;

public class PlayerInput
{
    public event Action tapStarted;
    public event Action tapEnded;
    public event Action tap;
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            tapStarted?.Invoke();

        if (Input.GetMouseButtonUp(0))
            tapEnded?.Invoke();

        if (Input.GetMouseButton(0))
            tap?.Invoke();
    }
}
