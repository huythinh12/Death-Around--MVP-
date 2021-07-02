using UnityEngine;
using TMPro;

public class CanvasStatus : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _txtPlayerName;
    [SerializeField]
    private TMP_Text _txtScore;
    [SerializeField]
    private TMP_Text _txtHealth;
    [SerializeField]
    private TMP_Text _txtLevel;

    private void Start()
    {
        if (DataPlayer.Instance)
        {
            //get name to show up on UI
            _txtPlayerName.text = DataPlayer.Instance._playerName;
        }
    }

    //Value updated from GameManager to show up on UI
    private void HandleStatus(int score, int health, int level)
    {
        if (health < 0)
            health = 0;
        _txtHealth.text = health.ToString();
        _txtScore.text = "Score: " + score.ToString();
        _txtLevel.text = "Level: " + level.ToString();
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
