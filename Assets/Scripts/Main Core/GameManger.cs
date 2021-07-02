using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Easy = 0,
    Normal = 1,
    Strong = 2,
    Boss = 3
}
public class GameManger : MonoBehaviour
{
    //Field
    //value to share with child class
    protected static bool _isGameActive;
    protected static int _score;
    protected static int _health;
    protected static bool _isChange = false;
    protected static bool _isInvisible = false;
    protected static bool _isExplosion = false;
    protected bool _isGameComplete = false;
    //Reference from out side
    [SerializeField]
    private GameObject _rightSpawn;
    [SerializeField]
    private GameObject _leftSpawn;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _iConLucky;
    [SerializeField]
    private List<Enemy> _enemys = new List<Enemy>();
    [SerializeField]
    private List<Items> _items = new List<Items>();
    [SerializeField]
    private AudioSource _musicBG;
    //field inside class
    private DataPlayer _dataPlayer;
    private Coroutine _invisibleCoroutine;
    private int _secondCountdown;
    private float _enemySpeed;
    private int _enemyHealth;
    private int _enemyIndex;
    private float _rateSpawn;
    private int _minRate;
    private int _maxRate;
    private float _waitToStart;
    private bool _isActiveItem = false;
    //Event for handle to CanvasUI
    public static event Action<int, int, int> EventOnStatus;
    public static event Action<int, bool> EventOnCanvasController;
    public static event Action<int, bool> EventOnTime;
    //PROP
    private int Level { set; get; }


    // Start is called before the first frame update
    private void Start()
    {
        _dataPlayer = FindObjectOfType<DataPlayer>();
        SetDifficulty();

        //Set value to UI when it begin  
        EventOnStatus?.Invoke(_score, _health, Level);

        //Start Game and get countdown time
        StartCoroutine(CountDown());
    }
    private void SetDifficulty()
    {
        int healthDifficulty;
        if (_dataPlayer._difficulty == 1)
        {
            healthDifficulty = 10;
            SetDefaultValue(healthDifficulty);
        }
        else if (_dataPlayer._difficulty == 2)
        {
            healthDifficulty = 7;
            SetDefaultValue(healthDifficulty);
        }
        else if (_dataPlayer._difficulty == 3)
        {
            healthDifficulty = 5;
            SetDefaultValue(healthDifficulty);

        }
    }
    private void SetDefaultValue(int healthDifficulty)
    {
        //All set default hear
        //music background
        //soundPlaying.volume = DataPlayer.Instance.volume;
        _musicBG.volume = _dataPlayer._volume;

        _secondCountdown = 31;
        //for state ui
        Level = 1;
        _health = healthDifficulty;
        _score = 0;
        //for enemy value
        _enemySpeed = 2;
        _enemyHealth = 1;
        _rateSpawn = 2f;
        //for random range
        _minRate = 5;
        _maxRate = 10;
        //other 
        _waitToStart = 1;
        _isGameActive = false;
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(2.5f);
        var waitTime = new WaitForSeconds(1);
        var saveSecond = _secondCountdown;
        _isGameActive = true;

        //When game is ready this stuff doing 
        WaveLevel();
        StartCoroutine(SpawnEnemyLevel());
        StartCoroutine(SpawnItems());

        //Count down for each wave  
        while (_secondCountdown > 0 && _health > 0)
        {
            _secondCountdown--;
            //invoke for update time from UI 
            EventOnTime?.Invoke(_secondCountdown, _isGameActive);
            yield return waitTime;
        }

        _isGameActive = false;
        //check condition for next wave 
        if (_health > 0)
        {
            Level++;
            if (Level <= 6)
            {
                EventOnTime?.Invoke(_secondCountdown, _isGameActive);
                _secondCountdown = saveSecond;
                _isChange = true;
                StartCoroutine(CountDown());
            }
            else
            {
                yield return new WaitForSeconds(2);
                _isGameComplete = true;
                GameFinished();
            }
        }

    }

    IEnumerator SpawnItems()
    {
        yield return new WaitForSeconds(_waitToStart);
        while (_isGameActive && _health > 0)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(_minRate, _maxRate));
            //check delay update to prevent timeout asynchronus
            yield return null;
            if (!_isGameActive) break;
            var randomItemState = UnityEngine.Random.Range(0, 3);
            Instantiate(_items[randomItemState], transform.position, Quaternion.identity);
        }
    }
    IEnumerator SpawnEnemyLevel()
    {
        yield return new WaitForSeconds(_waitToStart);
        var waitTime = new WaitForSeconds(_rateSpawn);
        //initialize value for random side
        Vector3 sideSpawn;
        Quaternion rotate = new Quaternion();

        while (_isGameActive && _health > 0)
        {
            //random spawn left or right
            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                sideSpawn = new Vector3(_rightSpawn.transform.position.x, UnityEngine.Random.Range(-4.5f, 5.4f), -0.5f);
                rotate.eulerAngles = new Vector3(0, -90, 0);
            }
            else
            {
                sideSpawn = new Vector3(_leftSpawn.transform.position.x, UnityEngine.Random.Range(-4.5f, 5.4f), -0.5f);
                rotate.eulerAngles = new Vector3(0, 90, 0);
            }

            //spawn enemy
            var enemy = Instantiate(_enemys[_enemyIndex], sideSpawn, rotate);
            //set value to enemy
            enemy._direction = _player.transform.position - enemy.transform.position;
            enemy._healthEnemy = _enemyHealth;
            enemy._speed = _enemySpeed;

            yield return waitTime;
        }
    }
    private void WaveLevel()
    {
        //set up for each wave 
        switch (Level)
        {
            case 1:
                _enemyIndex = (int)EnemyState.Easy;
                break;
            case 2:
                _enemyIndex = (int)EnemyState.Easy;
                _rateSpawn = 1.5f;
                break;
            case 3://this is boss
                _enemyIndex = (int)EnemyState.Strong;
                _rateSpawn = 1;
                _enemySpeed = 2.5f;
                break;
            case 4:
                RandomTypeEnemy((int)EnemyState.Easy, (int)EnemyState.Normal);
                _enemySpeed = 2.8f;
                break;
            case 5:
                RandomTypeEnemy((int)EnemyState.Strong, (int)EnemyState.Normal);
                break;
            default://this is boss
                _rateSpawn = 0.8f;
                _enemySpeed = 3;
                _enemyIndex = (int)EnemyState.Boss;
                break;
        }
    }

    private void RandomTypeEnemy(int enemy1, int enemy2)
    {
        if (UnityEngine.Random.Range(0, 2) == 1)
        {
            _enemyIndex = enemy1;
        }
        else
        {
            _enemyIndex = enemy2;
        }
    }

    private void Update()
    {
        if (_isGameActive == false && _iConLucky)
        {
            _iConLucky.SetActive(false);
        }
        //if what happening change this will update
        if (_isChange)
        {
            if (_health <= 0) { _isGameActive = false; Destroy(_player); };
            EventOnStatus?.Invoke(_score, _health, Level);
            _isExplosion = false;
            _isChange = false;
        }
        //check for invisible skill when get type of item
        if (_isInvisible && _isActiveItem == false)
        {
            _isActiveItem = true;
            _invisibleCoroutine = StartCoroutine(InvisibleTime());
        }

    }

    IEnumerator InvisibleTime()
    {
        _iConLucky.SetActive(true);
        yield return new WaitForSeconds(4);
        _iConLucky.SetActive(false);
        _isInvisible = false;
        _isActiveItem = false;

    }

    public void GameFinished()
    {
        if (_score > DataPlayer.Instance._score)
        {
            DataPlayer.Instance._score = _score;
            DataPlayer.Instance.SavePlayerData();

        }
        EventOnCanvasController?.Invoke(_health, _isGameComplete);
    }

}
