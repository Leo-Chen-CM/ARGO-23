using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    private int score = 0;
    public TMP_Text scoreText;

    private void Start()
    {
        scoreText.SetText("score: " + score.ToString());
    }

    public void addScore()
    {
        score++;
        scoreText.SetText("score: " + score.ToString());
    }
}
