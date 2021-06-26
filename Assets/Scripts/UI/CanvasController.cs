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
    [SerializeField]
    private AudioSource soundGameFinish;
    [SerializeField]
    private AudioClip onPickUpGameOver , onPickUpGameCompleted;
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
            soundGameFinish.PlayOneShot(onPickUpGameOver, 1);
            canvasGamOver.SetActive(true);
        }
        else if (isGameComplete)
        {
            soundGameFinish.PlayOneShot(onPickUpGameCompleted, 1);
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
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Playing");
    }
}
