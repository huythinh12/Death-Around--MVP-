using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject _canvasGamOver;
    [SerializeField]
    private GameObject _canvasCompleted;
    [SerializeField]
    private GameObject _penalMenu;
    [SerializeField]
    private AudioSource _soundGameFinish;
    [SerializeField]
    private AudioClip _onPickUpGameOver , _onPickUpGameCompleted;
    [SerializeField]
    private ParticleSystem _particleCompleted;
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
            _soundGameFinish.PlayOneShot(_onPickUpGameOver, 1);
            _canvasGamOver.SetActive(true);
        }
        else if (isGameComplete)
        {
            GameObject.Find("MouseControl").SetActive(false);
            _particleCompleted.Play();
            _soundGameFinish.PlayOneShot(_onPickUpGameCompleted, 1);
            _canvasCompleted.SetActive(true);
        }
    }
    public void OnClickMenu()
    {
        _penalMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        _penalMenu.SetActive(false);
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
