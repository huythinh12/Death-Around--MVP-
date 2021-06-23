using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField playerName;
    [SerializeField]
    private TMP_Text txtHightScore;
    [SerializeField]
    private GameObject canvasMainStart;
    [SerializeField]
    private GameObject canvasOption;
    [SerializeField]
    private DataPlayer dataPlayer;
    [SerializeField]
    private Slider sliderVolume;
    private int difficultySelect;
    private float volume;

    // Start is called before the first frame update
    void Start()
    {
        playerName.text = DataPlayer.Instance.playerName;
        txtHightScore.text = DataPlayer.Instance.score.ToString();
    }

    //will call when click Start button
    public void StartGame()
    {
        SceneManager.LoadScene("Playing");
    }

    public void OnClickOption()
    {
        canvasMainStart.SetActive(false);
        canvasOption.SetActive(true);
    }
    public void SaveAndExit()
    {
        canvasOption.SetActive(false);
        canvasMainStart.SetActive(true);
        dataPlayer.SaveDifficulty(difficultySelect);
    }
    public void Easy()
    {
        difficultySelect = 1;
    }
    public void Normal()
    {
        difficultySelect = 2;

    }
    public void Hard()
    {
        difficultySelect = 3;

    }
    public void ChangeVolume(float volume)
    {
        dataPlayer.msxBG.volume = volume;
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
