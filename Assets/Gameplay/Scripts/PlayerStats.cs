using System;

public class PlayerStats
{
    public float Points {get; private set;}
    public event Action<float>pointsChanged;

    public void ChangePoints(float points)
    {
        if (Points==points)
            return;

        Points = points;

        if (Points<0)
            Points = 0;

        pointsChanged?.Invoke(Points);
    }
}
