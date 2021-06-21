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

    private void HandleStatus(string playerName,int score,int health)
    {
        txtPlayerName.SetText(playerName);

        txtScore.text = score.ToString();

        txtHealth.text = health.ToString();

    }
    private void OnEnable()
    {
        GameManger.EventOnStatus += HandleStatus;
    }
}
