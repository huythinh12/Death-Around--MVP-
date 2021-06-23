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
    private bool isRunning = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }
    private void Update()
    {
        if (!isGameActive && !isRunning)
        {
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
                health--;
            }
            isChange = true;
            if (health <= 0)
            {
                GameFinished();
            }
            Destroy(gameObject);
        }
        //enemy collider with mouse
        else if (other.CompareTag("Mouse"))
        {
            healthEnemy--;
            if (healthEnemy <= 0)
            {
                score++;
                isChange = true;
                Destroy(gameObject);
            }
        }
    }

}
