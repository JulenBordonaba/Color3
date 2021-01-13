using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour{

    public static ScoreData record;

    public ScoreData currentScore = new ScoreData();
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = currentScore.score.ToString();
    }

    public void AddScore() 
    {
        currentScore.score++;
        scoreText.text = currentScore.score.ToString();
    }

    public void CheckRecord()
    {
        if(currentScore.score > record.score)
        {
            SetRecord();
        }
    }

    private void SetRecord()
    {
        record.score = currentScore.score;
    }

    public void ResetScore()
    {
        currentScore.score = 0;
        scoreText.text = currentScore.score.ToString();
    }
}
