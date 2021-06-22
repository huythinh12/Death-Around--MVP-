using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    [SerializeField]
    private string playerName;
    [SerializeField]
    private int score;
    [SerializeField]
    private int health;
    [SerializeField]
    private int level;

    //Event for handle to CanvasStatus
    public static event Action<string,int,int,int> EventOnStatus;
    // Start is called before the first frame update
    private void Start()
    {
        playerName = DataPlayer.Instance.playerName;
        EventOnStatus?.Invoke(playerName, score, health,level);
    }

    public void GameFinished()
    {
        DataPlayer.Instance.score = score;
        DataPlayer.Instance.SavePlayerData();
    }
}
