using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour{

    public int score = 0;
    public Text scoreText;

    private void Update()
    {
        scoreText.text = score.ToString();
    }

    public void AddScore() 
    {
        score++;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
