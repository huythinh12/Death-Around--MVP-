using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EnemyState
{
    Easy = 0,
    Normal = 1,
    Strong = 2,
    Boss = 3
}
public class GameManger : MonoBehaviour
{
    //Reference from out side
    [SerializeField]
    private GameObject rightSpawn;
    [SerializeField]
    private GameObject leftSpawn;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject iConLucky;
    [SerializeField]
    private List<Enemy> enemys = new List<Enemy>();
    [SerializeField]
    private List<Items> items = new List<Items>();
    [SerializeField]
    private AudioSource musicBG;
    private DataPlayer dataPlayer;

    //Field
    private int Level { set; get; }
    private int secondCountdown;
    private float enemySpeed;
    private int enemyHealth;
    private int enemyIndex;
    private float rateSpawn;
    private int minRate;
    private int maxRate;
    private float waitToStart;
    private bool isActiveItem = false;

    //value to share with child class
    protected static bool isGameActive = false;
    protected static int score;
    protected static int health;
    protected static bool isChange = false;
    protected static bool isInvisible = false;
    protected static bool isExplosion = false;
    protected bool isGameComplete = false;

    //Event for handle to CanvasUI
    public static event Action<int, int, int> EventOnStatus;
    public static event Action<int, bool> EventOnCanvasController;
    public static event Action<int, bool> EventOnTime;

    // Start is called before the first frame update
    private void Start()
    {
        dataPlayer = FindObjectOfType<DataPlayer>();
        SetDifficulty();

        //Set value to UI when it begin  
        EventOnStatus?.Invoke(score, health, Level);

        //Start Game and get countdown time
        StartCoroutine(CountDown());
    }
    private void SetDifficulty()
    {
        int healthDifficulty;
        if (dataPlayer.difficulty == 1)
        {
            healthDifficulty = 10;
            SetDefaultValue(healthDifficulty);
        }
        else if (dataPlayer.difficulty == 2)
        {
            healthDifficulty = 7;
            SetDefaultValue(healthDifficulty);
        }
        else if (dataPlayer.difficulty == 3)
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
        musicBG.volume = dataPlayer.volume;

        secondCountdown = 31;
        //for state ui
        Level = 1;
        health = healthDifficulty;
        score = 0;
        //for enemy value
        enemySpeed = 2;
        enemyHealth = 1;
        rateSpawn = 2f;
        //for random range
        minRate = 5;
        maxRate = 10;
        //other 
        waitToStart = 1;
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(2.5f);
        var waitTime = new WaitForSeconds(1);
        var saveSecond = secondCountdown;
        isGameActive = true;

        //When game is ready this stuff doing 
        WaveLevel();
        StartCoroutine(SpawnEnemyLevel());
        StartCoroutine(SpawnItems());

        //Count down for each wave  
        while (secondCountdown > 0 && health > 0)
        {
            secondCountdown--;
            //invoke for update time from UI 
            EventOnTime?.Invoke(secondCountdown, isGameActive);
            yield return waitTime;
        }

        isGameActive = false;
        //check condition for next wave 
        if (health > 0)
        {
            Level++;
            if (Level <= 6)
            {
                EventOnTime?.Invoke(secondCountdown, isGameActive);
                secondCountdown = saveSecond;
                isChange = true;
                StartCoroutine(CountDown());
            }
            else
            {
                yield return new WaitForSeconds(2);
                isGameComplete = true;
                GameFinished();
            }
        }

    }

    IEnumerator SpawnItems()
    {
        yield return new WaitForSeconds(waitToStart);
        while (isGameActive && health > 0)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minRate, maxRate));
            //yield return new WaitForSeconds(1);
            var randomItemState = UnityEngine.Random.Range(0, 3);
            Instantiate(items[randomItemState], transform.position, Quaternion.identity);
        }
    }
    IEnumerator SpawnEnemyLevel()
    {
        yield return new WaitForSeconds(waitToStart);
        var waitTime = new WaitForSeconds(rateSpawn);
        //initialize value for random side
        Vector3 sideSpawn;
        Quaternion rotate = new Quaternion();

        while (isGameActive && health > 0)
        {
            //random spawn left or right
            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                sideSpawn = new Vector3(rightSpawn.transform.position.x, UnityEngine.Random.Range(-4.5f, 5.4f), -0.5f);
                rotate.eulerAngles = new Vector3(0, -90, 0);
            }
            else
            {
                sideSpawn = new Vector3(leftSpawn.transform.position.x, UnityEngine.Random.Range(-4.5f, 5.4f), -0.5f);
                rotate.eulerAngles = new Vector3(0, 90, 0);
            }

            //spawn enemy
            var enemy = Instantiate(enemys[enemyIndex], sideSpawn, rotate);
            //set value to enemy
            enemy.direction = player.transform.position - enemy.transform.position;
            enemy.healthEnemy = enemyHealth;
            enemy.speed = enemySpeed;

            yield return waitTime;
        }
    }
    private void WaveLevel()
    {
        //set up for each wave 
        switch (Level)
        {
            case 1:
                enemyIndex = (int)EnemyState.Easy;
                break;
            case 2:
                enemyIndex = (int)EnemyState.Easy;
                rateSpawn = 1.5f;
                break;
            case 3://this is boss
                enemyIndex = (int)EnemyState.Strong;
                rateSpawn = 1;
                enemySpeed = 2.5f;
                break;
            case 4:
                RandomTypeEnemy((int)EnemyState.Easy, (int)EnemyState.Normal);
                enemySpeed = 2.8f;
                break;
            case 5:
                RandomTypeEnemy((int)EnemyState.Strong, (int)EnemyState.Normal);
                break;
            default://this is boss
                rateSpawn = 0.8f;
                enemySpeed = 3;
                enemyIndex = (int)EnemyState.Boss;
                break;
        }
    }

    private void RandomTypeEnemy(int enemy1,int enemy2)
    {
        if (UnityEngine.Random.Range(0, 2) == 1)
        {
            enemyIndex = enemy1;

        }
        else
        {
            enemyIndex = enemy2;
        }
    }

    private void Update()
    {
        //if what happening change this will update
        if (isChange)
        {
            EventOnStatus?.Invoke(score, health, Level);
            isExplosion = false;
            isChange = false;
        }
        //check for invisible skill when get type of item
        if (isInvisible && !isActiveItem)
        {
            StartCoroutine(InvisibleTime());
            isActiveItem = true;
        }
    }

    IEnumerator InvisibleTime()
    {
        iConLucky.SetActive(true);
        yield return new WaitForSeconds(4);
        iConLucky.SetActive(false);
        isInvisible = false;
        isActiveItem = false;

    }

    public void GameFinished()
    {
        DataPlayer.Instance.score = score;
        DataPlayer.Instance.SavePlayerData();
        EventOnCanvasController?.Invoke(health, isGameComplete);
    }

}
