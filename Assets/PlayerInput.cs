using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : SingleTone<PlayerInput>
{
    public event Action tapStarted;
    public event Action tapEnded;

    private void Awake() {
        gameObject.name = "Player input";
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            tapStarted?.Invoke();
        if (Input.GetMouseButtonUp(0))
            tapEnded?.Invoke();
    }
}
