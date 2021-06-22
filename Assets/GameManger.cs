using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    [SerializeField]
    private int score;
    [SerializeField]
    private int health;
    [SerializeField]
    private int level;

    //Event for handle to CanvasStatus
    public static event Action<int,int,int> EventOnStatus;

    // Start is called before the first frame update
    private void Start()
    {
 
        //setup event with 3 value 
        EventOnStatus?.Invoke(score, health,level);
    }

    public void GameFinished()
    {
        DataPlayer.Instance.score = score;
        DataPlayer.Instance.SavePlayerData();
    }
}
