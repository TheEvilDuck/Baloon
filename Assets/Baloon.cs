using UnityEngine;
using System;

public class Baloon : MonoBehaviour
{
    private bool _grow = false;
    private float _step = 0.01f;
    private float _currentGrow = 1f;
    private float _maxSizeInStartSizePercent = 10f;
    private Vector3 _startSize;
    private Transform _transform;
    private bool _exploded = false;

    public event Action grown;
    public event Action exploded;

    [SerializeField]Transform _baloonBody;
    [SerializeField]Transform _baloonGrip;
    [SerializeField]float _gripOffset = 2f;
    [SerializeField] Animator _animator;
    [SerializeField]GameObject _light;
    [SerializeField]GameObject _prticles;

    private Game _game;
    private void OnDisable() 
    {
       if (_game!=null)
        {
            _game.playerBreathStarted-=OnBreathStarted;
            _game.playerBreathEnded-=OnBreathEnded;
        }
    }

    private void OnBreathStarted()
    {
        _grow = true;
    }
    private void OnBreathEnded()
    {
        _grow = false;
    }

    private void Awake() 
    {
        _startSize = _baloonBody.localScale;
        _transform = transform;
        UpdateBaloonVisuals();
    }

    private void UpdateBaloonVisuals()
    {
        _baloonBody.localScale = _startSize*_currentGrow;
        _baloonGrip.localPosition = -new Vector3(0,_baloonBody.lossyScale.y/2f+_gripOffset,0);
    }
    private void Grow()
    {
        _currentGrow+=_step;
        if (_currentGrow>=_maxSizeInStartSizePercent)
        {
            _animator.SetBool("Exploded",true);
            _baloonGrip.gameObject.SetActive(false);
            _light.SetActive(false);
            _exploded = true;
            _prticles.transform.localScale = _baloonBody.localScale;
            _prticles.SetActive(true);
            exploded?.Invoke();
        }
        else
        {
            UpdateBaloonVisuals();
            grown?.Invoke();
        }
    }
    void Update()
    {
        if (_grow&&!_exploded)
            Grow();
        if (_exploded)
        {
           // _transform.position+=new Vector3(_transform.position.x+MathF.Cos(Time.time),_transform.position.y+MathF.Sin(Time.time),0)*0.01f;
        }
    }

    public void SetValues(float growStep,float maxSize, Game game)
    {
        _step = maxSize*growStep;
        _maxSizeInStartSizePercent = maxSize;
        _game = game;
        _game.playerBreathStarted+=OnBreathStarted;
        _game.playerBreathEnded+=OnBreathEnded;
        UpdateBaloonVisuals();

    }
    public void UpdateBaloonColor(Color color)
    {
        _baloonBody.GetComponent<SpriteRenderer>().color = color;
        _baloonGrip.GetComponent<SpriteRenderer>().color = color;
    }
}
