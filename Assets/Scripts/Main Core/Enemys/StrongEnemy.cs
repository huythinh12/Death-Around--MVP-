using UnityEngine;

public class StrongEnemy : Enemy
{
    [SerializeField]
    private GameObject _effect;
    private bool _isRunning = false;
    private Rigidbody _rigidbodyStrongEnemy;

    private void Start()
    {
        _rigidbodyStrongEnemy = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (!_isGameActive && !_isRunning)
        {
            Instantiate(_effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            _isRunning = true;
        }
        else
        {
            if (Time.timeScale != 0)
                _rigidbodyStrongEnemy.AddForce(_direction.normalized * _speed);
        }

        //check if item Explosion active then get effect
        if (_isExplosion && !_isRunning)
        {
            _score++;
            _isChange = true;
            _isRunning = true;
            Instantiate(_effect, transform.position, Quaternion.identity);
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
            Instantiate(_effect, transform.position, Quaternion.identity);
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
                Instantiate(_effect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    protected override void AddScoreWithTypeEnemy()
    {
        _score+=3;
    }

    protected override void GetDamgeWithTypeEnemy()
    {
        _health-=3;
    }

}
