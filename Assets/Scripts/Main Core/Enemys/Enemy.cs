using UnityEngine;

public abstract class Enemy : GameManger
{
  
    [HideInInspector]
    public int _healthEnemy;
    [HideInInspector]
    public float _speed;
    [HideInInspector]
    public int _damge;
    [HideInInspector]
    public int _enemyScore;
    [HideInInspector]
    public Vector3 _direction;
    protected Vector3 showUpPos;
    protected abstract void GetDamgeWithTypeEnemy();
    
    protected abstract void AddScoreWithTypeEnemy();
   
}
