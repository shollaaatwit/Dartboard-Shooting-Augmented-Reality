using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    public int scoreNumber = 0;

    void Start()
    {
        UpdateScoreText();
    }

    public void ChangeScore(int points)
    {
        scoreNumber += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        score.text = scoreNumber.ToString();
    }

    public int ReturnScore()
    {
        return scoreNumber;
    }

}
