using UnityEngine;

public class EasyEnemy : Enemy
{
    [SerializeField]
    private GameObject effect;
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

    protected override void AddScoreWithTypeEnemy()
    {
        score++;
    }

    protected override void GetDamgeWithTypeEnemy()
    {
        health--;
    }


}
