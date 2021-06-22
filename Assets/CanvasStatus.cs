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

    private void Start()
    {
        if (DataPlayer.Instance)
        {
            //get name to show up on UI
            txtPlayerName.text = DataPlayer.Instance.playerName;
        }
    }

    //Set value from GameManager to show up on UI
    private void HandleStatus(int score, int health, int level)
    {
        txtHealth.text = health.ToString();
        txtScore.text = "Score: " + score.ToString();
        txtLevel.text = "Level: " + level.ToString();
    }

    private void OnEnable()
    {
        GameManger.EventOnStatus += HandleStatus;
    }
}
