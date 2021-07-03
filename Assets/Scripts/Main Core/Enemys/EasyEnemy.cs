using UnityEngine;

public class EasyEnemy : Enemy
{
    [SerializeField]
    private GameObject _effect;
    [SerializeField]
    private ShowUp _showUP;
    private Rigidbody _rigidbodyEasyEnemy;
    private bool _isRunning = false;

    private void Start()
    {
        _rigidbodyEasyEnemy = GetComponent<Rigidbody>();
        
    }
    private void Update()
    {
        if (_isGameActive == false && _isRunning == false)
        {
            Instantiate(_effect, transform.position, Quaternion.identity);
          
            Destroy(gameObject);
            _isRunning = true;
        }
        else
        {
            if (Time.timeScale != 0)
                _rigidbodyEasyEnemy.AddForce(_direction.normalized * _speed);
        }

        //check if item Explosion active then get effect
        if (_isExplosion && _isRunning == false)
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
        //enemy collide with player
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
        //enemy collide with mouse
        else if (other.CompareTag("Mouse"))
        {
            _healthEnemy--;
            if (_healthEnemy <= 0)
            {
                AddScoreWithTypeEnemy();

                _isChange = true;
                Instantiate(_effect, transform.position, Quaternion.identity);
                showUpPos = new Vector3(transform.position.x, transform.position.y + 0.3f, 0);
                var showUp = Instantiate(_showUP, showUpPos, Quaternion.identity);
                showUp.showUpScore = _enemyScore.ToString();
                    Destroy(gameObject);
            }
        }
    }

    protected override void AddScoreWithTypeEnemy()
    {
        _score += _enemyScore;

    }

    protected override void GetDamgeWithTypeEnemy()
    {
        _health -= _damge;
    }


}
