using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasTimer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text txtTime;
    [SerializeField]
    private TMP_Text txtNotification;
    public static event Action<bool> EventTimer;

    private void OnEnable()
    {
        GameManger.EventOnTime += HandleTimer;
    }
  
    private void HandleTimer(int time,bool isGameActive)
    {
        txtTime.text = "Time: " + time;
        //time out 
        if (time <= 0 && !isGameActive)
        {
            StartCoroutine(Notification());
        }
    }
    IEnumerator Notification()
    {
        yield return new WaitForSeconds(0.5f);
        txtNotification.text = "Success";
        yield return new WaitForSeconds(1f);
        txtNotification.text = "";
    }
    private void OnDisable()
    {
        GameManger.EventOnTime -= HandleTimer;
    }
}
