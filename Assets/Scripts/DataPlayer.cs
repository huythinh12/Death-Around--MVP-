using System;
using System.IO;
using UnityEngine;


public class DataPlayer : MonoBehaviour
{
    public static DataPlayer Instance;
    public string playerName;
    [HideInInspector]
    public int score;
    public int difficulty = 1;
    public float volume;

    private void Awake()
    {
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
    //this is call from inputfield
    public void EnterPlayerName(string name)
    {
        playerName = name;
    }

    public void SavePlayerData()
    {
        //check all progressing when write file data to system
        try
        {
            Player data = new Player();
            data.playerName = playerName;
            data.score = score;
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
                playerName = data.playerName;
                score = data.score;
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
