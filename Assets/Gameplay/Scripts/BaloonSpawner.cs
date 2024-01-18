using System;

public class BaloonSpawner
{
    public event Action baloonSpawned;
    private BaloonFactory _baloonFactory;
    private Baloon _currentBaloon;

    public Baloon CurrentBaloon => _currentBaloon;

    public BaloonSpawner(BaloonFactory baloonFactory)
    {
        _baloonFactory = baloonFactory;
    }

    public void SpawnBaloon()
    {
        _currentBaloon = _baloonFactory.Get();
        baloonSpawned?.Invoke();
    }
}
