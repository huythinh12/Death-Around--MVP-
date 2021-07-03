using System;
using System.IO;
using UnityEngine;


public class DataPlayer : MonoBehaviour
{
    public static DataPlayer Instance;
    public string _playerName;
    [HideInInspector]
    public int _score;
    public int _difficulty;
    public float _volume;

    private void Awake()
    {
        _difficulty = 1;
        _volume = 1;
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //load data when awake
        LoadPlayerData();
    }


    public void SavePlayerData()
    {
        //check all progressing when write file data to system
        try
        {
            Player data = new Player();
            data.playerName = _playerName;
            data.score = _score;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savedataplayer-da.json", json);
        }
        catch (Exception)
        {

            Debug.LogError("Cannot save file");
        }
    }

    public void LoadPlayerData()
    {
        //check all progressing when get file data from system
        try
        {
            string path = Application.persistentDataPath + "/savedataplayer-da.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Player data = JsonUtility.FromJson<Player>(json);
                _playerName = data.playerName;
                _score = data.score;
            }
        }
        catch (Exception)
        {

            Debug.LogError("Cannot load file");
        }

    }

    [Serializable]
    public class Player// initialize data player for json
    {
        public string playerName = "";
        public int score = 0;
    }
}
