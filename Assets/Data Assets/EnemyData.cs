using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData", menuName ="Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public string description;
    public Enemy enemyPrefab;
    public int _health = 1;
    public float _speed = 1;
    public int _damge = 1;
    public int _scrore = 1;
}
