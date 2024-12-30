using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class DartLevelManager : MonoBehaviour
{
    public bool gameStarted = false;
    public bool isNewPlayer;
    public ScoreManager scoreManager;
    public BestScoreTracker bestScoreTracker;
    public string currentScene;
    public TextMeshProUGUI timerText;
    public float timeRemaining = 60f;
    public int firstTimeLoad = 0;
    public AudioSource tutorialAudio;
    public GameObject oldButton;
    public GameObject newPrefab;
    public GameObject newPrefab2;

    public AudioSource gameOverSound;
    public AudioSource winnerSound;

    void Start()
    {
        LoadBestScore();
        //load up position on spacial anchor TODO
        firstTimeLoad = PlayerPrefs.GetInt("FirstTimePlayer", 0);
        if(firstTimeLoad == 0)
        {
            tutorialAudio.Play();
            firstTimeLoad = 1;
        }
        currentScene = SceneManager.GetActiveScene().name;
        oldButton.SetActive(true);
        newPrefab.SetActive(false);
        newPrefab2.SetActive(false);        
    }

    void Update()
    {
        if (gameStarted && timeRemaining > 0.4f)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        if(firstTimeLoad == 0)
        {
            isNewPlayer = true;
        }
        else if(firstTimeLoad == 1)
        {
            isNewPlayer = false;
        }
        UpdateMenuUI();
    }

    public void SaveAllInfo()
    {
        PlayerPrefs.SetInt("BestScore", bestScoreTracker.currentBestScore);
        PlayerPrefs.SetInt("FirstTimePlayer", firstTimeLoad);
        PlayerPrefs.Save();
        //saves info before resetting scene
    }

    public void RestartScene()
    {
        SaveAllInfo();
        SceneManager.LoadScene(currentScene);
    }

    public void ResetScene()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(currentScene);
    }

    public void IsGameOver()
    {
        if(timeRemaining <= 0.6f)
        {
            gameStarted = false;
            if(bestScoreTracker.currentBestScore > bestScoreTracker.currentScore)
            {
                gameOverSound.Play();
            }
            else
            {
                winnerSound.Play();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            // Format the time as minutes:seconds and update the UI text
            // int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{00}", seconds);
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        UpdateMenuUI();
    }

    public void UpdateMenuUI()
    {
        if(gameStarted == true && newPrefab.activeInHierarchy == false)
        {
            oldButton.SetActive(false);
            newPrefab.SetActive(true);
            newPrefab2.SetActive(true);
        }
    }


    private void LoadBestScore()
    {
        bestScoreTracker.currentBestScore = PlayerPrefs.GetInt("BestScore", 0);
        Debug.Log($"Best Score Loaded: {bestScoreTracker.currentBestScore}");
    }


    void OnApplicationQuit()
    {
        SaveAllInfo();
    }

    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            SaveAllInfo();
        }
    }
}
