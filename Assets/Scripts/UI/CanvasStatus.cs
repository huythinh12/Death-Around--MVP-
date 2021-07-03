using System.Text;
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
    private StringBuilder sbScore = new StringBuilder("Score: x"); 
    private StringBuilder sbLevel = new StringBuilder("Level: x"); 

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

        _txtScore.text = sbScore.Replace("x",score.ToString()).ToString();
        _txtLevel.text = sbLevel.Replace("x",level.ToString()).ToString();

        sbScore.Clear();
        sbLevel.Clear();
        sbScore.Append("Score: x");
        sbLevel.Append("Level: x");
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
