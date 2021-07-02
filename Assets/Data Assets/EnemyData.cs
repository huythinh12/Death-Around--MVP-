using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData", menuName ="Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public string description;
    public GameObject enemyPrefab;
    public int _health = 1;
    public float _speed = 1;
}
