using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : MonoBehaviour
{
    private bool _grow = false;
    private Transform _transform;
    private float _step = 0.01f;
    private float _maxSizeInStartSizePercent = 1.5f;
    private Vector3 _startSize;
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
        if ((_transform.localScale+Vector3.one*_step).x<(_startSize+Vector3.one*_maxSizeInStartSizePercent).x)
            _transform.localScale+=Vector3.one*_step;
        else
            Debug.Log("BOOM");
    }
    // Update is called once per frame
    void Update()
    {
        if (_grow)
            Grow();
    }
}
