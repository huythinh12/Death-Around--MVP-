using UnityEngine;
using TMPro;

public class CanvasStatus : MonoBehaviour
{
    [SerializeField]
    private TMP_Text txtPlayerName;
    [SerializeField]
    private TMP_Text txtScore;
    [SerializeField]
    private TMP_Text txtHealth;
    [SerializeField]
    private TMP_Text txtLevel;

    private void Start()
    {
        if (DataPlayer.Instance)
        {
            //get name to show up on UI
            txtPlayerName.text = DataPlayer.Instance.playerName;
        }
    }

    //Value updated from GameManager to show up on UI
    private void HandleStatus(int score, int health, int level)
    {
        if (health < 0)
            health = 0;
        txtHealth.text = health.ToString();
        txtScore.text = "Score: " + score.ToString();
        txtLevel.text = "Level: " + level.ToString();
    }

    private void OnEnable()
    {
        GameManger.EventOnStatus += HandleStatus;
    }

    private void OnDisable()
    {
        GameManger.EventOnStatus -= HandleStatus;
    }
}
