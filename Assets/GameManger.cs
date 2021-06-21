using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    [SerializeField]
    private string playerName;
    [SerializeField]
    private int score;
    [SerializeField]
    private int health;
    public static event Action<string,int,int> EventOnStatus;
    // Start is called before the first frame update
    private void Start()
    {
        playerName = DataPlayer.Instance.playerName;
        EventOnStatus?.Invoke(playerName, score, health);

    }
 
    private void Update()
    {
      
    }
}
