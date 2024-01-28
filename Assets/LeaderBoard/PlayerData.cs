using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public readonly string Name;
    public readonly int Score;

    public PlayerData(string name, int score)
    {
        Name = name;
        Score = score;
    }
}
