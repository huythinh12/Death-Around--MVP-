using UnityEngine;

public abstract class Enemy : GameManger
{
    [HideInInspector]
    public int _healthEnemy;
    [HideInInspector]
    public float _speed;
    [HideInInspector]
    public Vector3 _direction;
    
    protected abstract void GetDamgeWithTypeEnemy();
    
    protected abstract void AddScoreWithTypeEnemy();
   
}
