using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    private int score = 0;
    public TMP_Text scoreText;
    public Slider healthbar;

    public float speed;
    public int health;
    private int maxHealth;

    private void Start()
    {
        scoreText.SetText("score: " + score.ToString());
        healthbar.maxValue = health;
        healthbar.value = health;
        maxHealth = health;
        if (scoreText != null)
        {
            scoreText.SetText("score: " + score.ToString());
        }
    }

    public void addScore()
    {
        if (scoreText != null)
        {
            score++;
            scoreText.SetText("score: " + score.ToString());
        }
    }

    public float getSpeed()
    {
        return speed;
    }

    public bool reduceHealth()
    {
        health--;
        healthbar.value = health;
        return health > 0;
    }

    public void Reset()
    {
        health = maxHealth;
        healthbar.value = health;
        score = 0;
    }
}
