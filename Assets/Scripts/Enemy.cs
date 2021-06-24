using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameManger
{
    [HideInInspector]
    public int healthEnemy;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public Vector3 direction;
    [SerializeField]
    private GameObject effect;
    private bool isRunning = false;
    private Rigidbody rb;
    private string nameEnemy;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        nameEnemy = transform.name;
    }
    private void Update()
    {
        if (!isGameActive && !isRunning)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            isRunning = true;
        }
        else
        {
            if (Time.timeScale != 0)
                rb.AddForce(direction.normalized * speed);
        }

        //check if item Explosion active then get effect
        if (isExplosion && !isRunning)
        {
            score++;
            isChange = true;
            isRunning = true;
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //enemy collider with player
        if (other.CompareTag("Player"))
        {
            if (!isInvisible)
            {
                GetDamgeWithTypeEnemy();
            }
            isChange = true;
            if (health <= 0)
            {
                GameFinished();
            }
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        //enemy collider with mouse
        else if (other.CompareTag("Mouse"))
        {
            healthEnemy--;
            if (healthEnemy <= 0)
            {
                AddScoreWithTypeEnemy();

                isChange = true;
                Instantiate(effect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
    private void GetDamgeWithTypeEnemy()
    {
        if (nameEnemy.StartsWith("EasyEnemy"))
        {
            health--;
        }
        else if (nameEnemy.StartsWith("NormalEnemy"))
        {
            health-=2;

        }
        else if (nameEnemy.StartsWith("StrongEnemy"))
        {
            health-=3;

        }
        else if (nameEnemy.StartsWith("BossEnemy"))
        {
            health-=4;
        }
    }
    private void AddScoreWithTypeEnemy()
    {
        if (nameEnemy.StartsWith("EasyEnemy"))
        {
            score++;
        }
        else if (nameEnemy.StartsWith("NormalEnemy"))
        {
            score += 2;
        }
        else if (nameEnemy.StartsWith("StrongEnemy"))
        {
            score += 3;
        }
        else if (nameEnemy.StartsWith("BossEnemy"))
        {
            score += 4;

        }
    }
}
