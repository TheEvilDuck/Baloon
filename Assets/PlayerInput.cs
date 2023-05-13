using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public event Action tapStarted;
    public event Action tapEnded;

    private static PlayerInput _instance;
    public static PlayerInput instance
    {
        get
        {
            if (_instance==null)
            {
                _instance = new GameObject().AddComponent<PlayerInput>();
            }
            return _instance;
        }
        private set{}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            tapStarted?.Invoke();
        if (Input.GetMouseButtonUp(0))
            tapEnded?.Invoke();
    }
}
