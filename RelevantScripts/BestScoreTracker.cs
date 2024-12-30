using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BestScoreTracker : MonoBehaviour
{
    public ScoreManager scoreManager;
    public int currentBestScore;
    public int currentScore;
    public TextMeshProUGUI score;
    
    void Start()
    {
        // do something to save and retrieve previous best score
    }

    void Update()
    {
        CheckBestScore();
    }

    public void CheckBestScore() 
    {
        if (scoreManager != null)
        {
            currentScore = scoreManager.ReturnScore();
            if(currentScore > currentBestScore)
            {
                currentBestScore = currentScore;
            }
        }
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        score.text = currentBestScore.ToString();
    }
}
