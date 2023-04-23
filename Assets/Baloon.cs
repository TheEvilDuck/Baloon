using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Baloon : MonoBehaviour
{
    private bool _grow = false;
    private Transform _transform;
    private float _step = 0.01f;
    private float _currentGrow = 1f;
    private float _maxSizeInStartSizePercent = 10f;
    private Vector3 _startSize;

    public event Action<float> grown;
    public event Action exploded;
    private void OnEnable() 
    {
        PlayerInput.instance.tapStarted+=OnTapStarted;
        PlayerInput.instance.tapEnded+=OnTapEnded;
    }
    private void OnDisable() 
    {
        if (PlayerInput.instance!=null)
        {
            PlayerInput.instance.tapStarted-=OnTapStarted;
            PlayerInput.instance.tapEnded-=OnTapEnded;
        }
    }

    private void OnTapStarted()
    {
        _grow = true;
    }
    private void OnTapEnded()
    {
        _grow = false;
    }

    private void Awake() 
    {
        _transform = transform;
        _startSize = _transform.localScale;
    }

    private void Grow()
    {
        _currentGrow+=_step;
        if (_currentGrow>=_maxSizeInStartSizePercent)
        {
            Debug.Log("BOOM");
            exploded?.Invoke();
        }
        else
        {
            _transform.localScale = _startSize*_currentGrow;
            grown?.Invoke(_currentGrow);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (_grow)
            Grow();
    }
}
