using UnityEngine;
using System;
public class Baloon : MonoBehaviour
{
    [SerializeField]private Transform _baloonBody;
    [SerializeField]private Transform _baloonGrip;
    [SerializeField]private GameObject _light;
    [SerializeField]private Animator _animator;

    public event Action grown;
    public event Action exploded;

    private float _step = 0.01f;
    private float _currentGrow = 1f;
    private float _maxSizePercent;
    private Vector3 _startSize;
    private float _gripOffset;
    private bool _grow = false;

    private void Update() 
    {
        if (!_grow)
            return;

        DoGrowStep();
    }

    public void Init(float growStep,float maxSizePercent, Color color, float gripOffset)
    {
        _step = maxSizePercent*growStep/100f;
        _maxSizePercent = maxSizePercent;
        _startSize = _baloonBody.localScale;
        _gripOffset = gripOffset;
        _currentGrow = 1f;
        UpdateSize();
        UpdateColor(color);

    }

    public void StartGrow() => _grow = true;
    public void StopGrow() => _grow = false;

    private void DoGrowStep()
    {
        _currentGrow+=_step;

        if (_currentGrow>=_maxSizePercent)
        {
            _animator.SetBool("Exploded", true);
            _baloonGrip.gameObject.SetActive(false);
            _light.SetActive(false);
            exploded?.Invoke();
        }
        else
        {
            UpdateSize();
            grown?.Invoke();
        }
    }
    private void UpdateColor(Color color)
    {
        _baloonBody.GetComponent<SpriteRenderer>().color = color;
        _baloonGrip.GetComponent<SpriteRenderer>().color = color;
    }

    private void UpdateSize()
    {
        _baloonBody.localScale = _startSize*_currentGrow;
        _baloonGrip.localPosition = -new Vector3(0,_baloonBody.lossyScale.y/2f+_gripOffset,0);
    }
}
