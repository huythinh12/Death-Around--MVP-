using System;
using System.Text;
using System.Collections;
using UnityEngine;
using TMPro;

public class CanvasTimer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _txtTime;
    [SerializeField]
    private TMP_Text _txtNotification;
    private int _waveCount = 1;
    private StringBuilder sbTime = new StringBuilder("Time: x");

    private void OnEnable()
    {
        GameManger.EventOnTime += HandleTimer;
    }
  
    private void HandleTimer(int time,bool isGameActive)
    {
        _txtTime.text = sbTime.Replace("x",time.ToString()).ToString();
        sbTime.Clear();
        sbTime.Append("Time: x");
        //time out 
        if (time <= 0 && !isGameActive)
        {
            StartCoroutine(Notification());
        }
    }
    IEnumerator Notification()
    {
        _waveCount++;
      
        yield return new WaitForSeconds(0.5f);
        _txtNotification.text = "Success";
        yield return new WaitForSeconds(1f);
        //check wave to show notification text when wave boss appear
        if (_waveCount == 3 || _waveCount == 6)
        {
            _txtNotification.text = "Boss Wave";
        }
        yield return new WaitForSeconds(1);
        _txtNotification.text = "";
    }
    private void OnDisable()
    {
        GameManger.EventOnTime -= HandleTimer;
    }
}
