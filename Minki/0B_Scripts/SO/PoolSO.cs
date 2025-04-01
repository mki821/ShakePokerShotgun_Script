using UnityEngine;

[CreateAssetMenu(menuName = "SO/PoolSO")]
public class PoolSO : ScriptableObject
{
    public new string name;
    public int count = 10;
    public GameObject prefab;
}
