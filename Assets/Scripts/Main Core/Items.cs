using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : GameManger
{
    private Rigidbody itemRb;
    public int pointValue;
    [SerializeField]
    private GameObject effectItemBlack, effectItemOrange, effectItemRed;
    [SerializeField]
    private AudioSource soundItem;
    [SerializeField]
    private AudioClip onPickUpItem;
    // Start is called before the first frame update
    void Start()
    {
        if (isGameActive)
        {
            soundItem.PlayOneShot(onPickUpItem, 1);
            itemRb = GetComponent<Rigidbody>();
            RandomTorque();
            RandomForce();
            Vector3 RandomPos = new Vector3(Random.Range(-11, 11), -8, -1f);
            transform.position = RandomPos;
        }
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
                Instantiate(effectItemOrange, transform.position, Quaternion.identity);
                isInvisible = true;
            }
            else if (transform.name.StartsWith("Bad"))
            {
                Instantiate(effectItemBlack, transform.position, Quaternion.identity);
                if (score > 0)
                {
                    score--;
                    isChange = true;
                }
            }
            else if (transform.name.StartsWith("Explosion"))
            {
                Instantiate(effectItemRed, transform.position, Quaternion.identity);
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
