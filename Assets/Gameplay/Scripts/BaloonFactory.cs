using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonFactory
{
    private BaloonConfig _config;

    public BaloonFactory(BaloonConfig config)
    {
        _config = config;
    }

    public Baloon Get()
    {
        Baloon baloon = UnityEngine.Object.Instantiate(_config.BaloonPrefab);

        float sizeMultiplier = UnityEngine.Random.Range(1f-_config.SizeMultiplierRange, 1f+_config.SizeMultiplierRange);

        Color randomColor = new Color(
            UnityEngine.Random.Range(0,1f),
            UnityEngine.Random.Range(0,1f),
            UnityEngine.Random.Range(0,1f)
        );

        baloon.Init(_config.GrowStep,_config.MaxBaloonSizePercent*sizeMultiplier, randomColor, _config.GripOffset);

        return baloon;
    }
}
