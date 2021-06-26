using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class CanvasTimer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text txtTime;
    [SerializeField]
    private TMP_Text txtNotification;
    public static event Action<bool> EventTimer;
    private int waveCount = 1;

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
        waveCount++;
      
        yield return new WaitForSeconds(0.5f);
        txtNotification.text = "Success";
        yield return new WaitForSeconds(1f);
        //check wave to show notification text when wave boss appear
        if (waveCount == 3 || waveCount == 6)
        {
            txtNotification.text = "Boss Wave";
        }
        yield return new WaitForSeconds(1);
        txtNotification.text = "";
    }
    private void OnDisable()
    {
        GameManger.EventOnTime -= HandleTimer;
    }
}
