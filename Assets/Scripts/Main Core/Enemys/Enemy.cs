using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : GameManger
{
    [HideInInspector]
    public int healthEnemy;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public Vector3 direction;
    
    protected abstract void GetDamgeWithTypeEnemy();
    
    protected abstract void AddScoreWithTypeEnemy();
   
}
