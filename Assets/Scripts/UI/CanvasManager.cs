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
    private TMP_InputField _playerName;
    [SerializeField]
    private TMP_Text _txtHightScore;
    [SerializeField]
    private GameObject _canvasMainStart;
    [SerializeField]
    private GameObject _canvasOption;
    [SerializeField]
    private GameObject _canvasTutorial;
    [SerializeField]
    private AudioSource _musicBG;
    [SerializeField]
    private Slider _sliderVolume;
    private DataPlayer _dataPlayer;
    private int _difficultySelect = 1;
    private float _volume;

    // Start is called before the first frame update
    void Start()
    {
        _playerName.text = DataPlayer.Instance._playerName;
        _txtHightScore.text = DataPlayer.Instance._score.ToString();
        _dataPlayer = FindObjectOfType<DataPlayer>();

        //set value volume default
        if(_dataPlayer._volume > 0)
        {
            _musicBG.volume = _dataPlayer._volume;
            _sliderVolume.value = _dataPlayer._volume;
        }
    }

    //will call when click Start button
    public void StartGame()
    {
        SceneManager.LoadScene("Playing");
    }

    public void OnClickOption()
    {
        _canvasMainStart.SetActive(false);
        _canvasOption.SetActive(true);
    }
    public void OnclickTutorial()
    {
        _canvasMainStart.SetActive(false);
        _canvasTutorial.SetActive(true);
    }
    public void SaveAndExit()
    {
        _canvasOption.SetActive(false);
        _canvasTutorial.SetActive(false);
        _canvasMainStart.SetActive(true);
        _dataPlayer._difficulty = _difficultySelect;
    }
    public void Easy()
    {
        _difficultySelect = 1;
    }
    public void Normal()
    {
        _difficultySelect = 2;

    }
    public void Hard()
    {
        _difficultySelect = 3;

    }
    public void ChangeVolume(float volume)
    {
        _musicBG.volume = volume;
        _dataPlayer._volume = volume;
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
