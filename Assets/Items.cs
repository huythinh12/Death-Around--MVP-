using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : GameManger
{
    private Rigidbody itemRb;
    public int pointValue;
    // Start is called before the first frame update
    void Start()
    {
        itemRb = GetComponent<Rigidbody>();
        RandomTorque();
        RandomForce();
        Vector3 RandomPos = new Vector3(Random.Range(-11, 11), -8, -1f);
        transform.position = RandomPos;

    }
    private void Update()
    {
        //check condition for destroy item
        if (!isGameActive || transform.position.y < -9)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //check condition when mouse hit item 
        if (other.CompareTag("Mouse"))
        {
            if (transform.name.StartsWith("Lucky"))
            {
                isInvisible = true;
            }
            else if (transform.name.StartsWith("Bad"))
            {
                if (score > 0)
                {
                    score--;
                    isChange = true;
                }
            }
            else if (transform.name.StartsWith("Explosion"))
            {
                isExplosion = true;
            }
            Destroy(gameObject);
        }
    }

    private void RandomTorque()
    {
        itemRb.AddTorque(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10), ForceMode.Impulse);
    }

    private void RandomForce()
    {
        itemRb.AddForce(Vector3.up * Random.Range(12, 16), ForceMode.Impulse);
    }
}
