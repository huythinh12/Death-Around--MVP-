using UnityEngine;
using TMPro;

public class CanvasStatus : MonoBehaviour
{
    [SerializeField]
    TMP_Text txtPlayerName;
    [SerializeField]
    TMP_Text txtScore;
    [SerializeField]
    TMP_Text txtHealth;
    [SerializeField]
    TMP_Text txtLevel;

    private void HandleStatus(string playerName,int score,int health,int level)
    {
        print(health);
        txtPlayerName.SetText(playerName);
        txtHealth.text = health.ToString();
        txtScore.text = "Score: " + score.ToString();
        txtLevel.text = "Level: " + level.ToString();
    }
    private void OnEnable()
    {
        GameManger.EventOnStatus += HandleStatus;
    }
}
