using UnityEngine;

public class NormalEnemy : Enemy
{
    [SerializeField]
    private GameObject effect;
    private bool isRunning = false;
    private Rigidbody rigidbodyNormalEnemy;

    private void Start()
    {
        rigidbodyNormalEnemy = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (!_isGameActive && !isRunning)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            isRunning = true;
        }
        else
        {
            if (Time.timeScale != 0)
                rigidbodyNormalEnemy.AddForce(_direction.normalized * _speed);
        }

        //check if item Explosion active then get effect
        if (_isExplosion && !isRunning)
        {
            _score++;
            _isChange = true;
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
            if (!_isInvisible)
            {
                GetDamgeWithTypeEnemy();
            }
            _isChange = true;
            if (_health <= 0)
            {
                GameFinished();
            }
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        //enemy collider with mouse
        else if (other.CompareTag("Mouse"))
        {
            _healthEnemy--;
            if (_healthEnemy <= 0)
            {
                AddScoreWithTypeEnemy();

                _isChange = true;
                Instantiate(effect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    protected override void AddScoreWithTypeEnemy()
    {
        _score+=2;
    }

    protected override void GetDamgeWithTypeEnemy()
    {
        _health-=2;
    }

}
