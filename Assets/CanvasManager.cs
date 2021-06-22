using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField playerName;
    [SerializeField]
    TMP_Text txtHightScore;
    // Start is called before the first frame update
    void Start()
    {
        playerName.text = DataPlayer.Instance.playerName;
        txtHightScore.text = DataPlayer.Instance.score.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Playing");
    }


    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
Application.Quit();
#endif
    }
}
