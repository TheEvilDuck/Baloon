using System;
using System.Collections;
using UnityEngine;

public class BreathController
{
    public event Action inhaleStarted;
    public event Action breathStarted;
    public event Action breathEnded;

    private readonly float _inhaleTime;

    private Coroutine _currentCoroutine;
    private MonoBehaviour _contextForCoroutine;

    public BreathController(MonoBehaviour contextForCoroutine, float inhaleTime)
    {
        _contextForCoroutine = contextForCoroutine;
        _inhaleTime = inhaleTime;
    }

    public void StartBreath() =>  _currentCoroutine = _contextForCoroutine.StartCoroutine(Breath());

    public void StopBreath()
    {
        if (_currentCoroutine!=null)
            _contextForCoroutine.StopCoroutine(_currentCoroutine);

        breathEnded?.Invoke();
            
    }

    private IEnumerator Breath()
    {
        inhaleStarted?.Invoke();
        Debug.Log("INHALE");

        yield return new WaitForSeconds(_inhaleTime);

        Debug.Log("BREATH");

        breathStarted?.Invoke();
    }


}
