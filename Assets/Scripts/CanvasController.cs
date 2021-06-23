using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject canvasGamOver;
    [SerializeField]
    private GameObject canvasCompleted;
    [SerializeField]
    private GameObject penalMenu;
    private void OnEnable()
    {
        GameManger.EventOnCanvasController += HandleCanvas;
    }
    private void OnDisable()
    {
        GameManger.EventOnCanvasController -= HandleCanvas;
    }
    private void HandleCanvas(int health, bool isGameComplete)
    {
        if (health <= 0)
        {
            canvasGamOver.SetActive(true);
        }
        else if (isGameComplete)
        {
            canvasCompleted.SetActive(true);
        }
    }

    public void OnClickMenu()
    {
        penalMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        penalMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Playing");
    }
}
