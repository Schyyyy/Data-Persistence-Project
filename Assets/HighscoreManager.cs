using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager instance;
    [SerializeField] TMP_InputField nameInput;
    public string name;
    public Highscore score;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        string path = Application.persistentDataPath + "/save.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            score = JsonUtility.FromJson<Highscore>(json);
        }
        if(score == null)
        {
            score = new Highscore();
            score.name = "";
            score.score = 0;
        }
    }

    public void SetName()
    {
        name = nameInput.text;
    }

    [System.Serializable]
    public class Highscore
    {
        public string name;
        public int score;
    }

    public void Save(int points)
    {
        score.name = HighscoreManager.instance.name;
        score.score = points;

        string json = JsonUtility.ToJson(score);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }
}
