using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Timer")] 
    public TextMeshProUGUI timerText;
    public float timer = 60;

    private int score = 0;
    
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;

            if (score > HighScore)
            {
                HighScore = score;
            }
        }
    }

    private int highScore = 0;

    private const string KEY_HIGH_SCORE = "HIGH SCORE";

    int HighScore
    {
        get
        {
            if (File.Exists(DATA_FULL_HS_FILE_PATH))
            {
                string fileContents = File.ReadAllText(DATA_FULL_HS_FILE_PATH);
                highScore = Int32.Parse(fileContents);
            }

            return highScore;
        }
        set
        {
            highScore = value;
            string fileContent = "" + highScore;

            if (!Directory.Exists(Application.dataPath + DATA_DIR))
            {
                Directory.CreateDirectory(Application.dataPath + DATA_DIR);
            }
            
            File.WriteAllText(DATA_FULL_HS_FILE_PATH, fileContent);
        }
    }

    private const string DATA_DIR = "/Data/";
    private const string DATA_HS_FILE = "hs.txt";
    private string DATA_FULL_HS_FILE_PATH;
    
    //score showing in UI
    public TextMeshProUGUI scoreText;
    
    private void Awake()
    {
        if (instance == null) //if the instance var has not been set
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else //if there is already a singleton of this type
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 10;
        score = 0;
        DATA_FULL_HS_FILE_PATH = Application.dataPath + DATA_DIR + DATA_HS_FILE;
    }

    // Update is called once per frame
    void Update()
    {
        //clear high score
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Delete");
            PlayerPrefs.DeleteKey(KEY_HIGH_SCORE);
        }
        
        //timer start
        timer -= Time.deltaTime;
        timerText.text = "" + Mathf.RoundToInt(timer) + "";
        
        //restart timer if time runs out
        if (timer <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            timer = 10;
            score = 0;
        }
        
        //print score text
        scoreText.text = "SCORE: " + score + "\nHIGH SCORE: " + HighScore;
    }
}
