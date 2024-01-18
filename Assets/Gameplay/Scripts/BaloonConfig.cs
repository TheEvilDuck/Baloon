using UnityEngine;

[CreateAssetMenu(fileName = "Baloon config", menuName = "Configs/New baloon config")]
public class BaloonConfig : ScriptableObject
{
    [field: SerializeField, Range(1.1f,20f)]public float MaxBaloonSizePercent{get;private set;} = 1.1f;
    [field:SerializeField, Range(0,0.99f)] public float SizeMultiplierRange{get; private set;} = 0;
    [field:SerializeField, Range(0.001f,1f)] public float GrowStep{get;private set;} = 0.01f;
    [field:SerializeField, Range(0,10f)] public float PointsPerStep{get;private set;} = 1f;
    [field:SerializeField, Range(1f, 10f)] public float HoldMultiplier{get;private set;} = 1.1f;
    [field:SerializeField, Range(0.1f, 5f)] public float HoldTimeToStartBreath{get;private set;} = 2f;
    [field:SerializeField] public float GripOffset{get;private set;}

    [field:SerializeField] public Baloon BaloonPrefab{get;private set;}
}
