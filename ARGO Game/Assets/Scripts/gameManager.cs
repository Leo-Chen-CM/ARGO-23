using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    private int score = 0;
    public TMP_Text scoreText;
    public Slider healthbar;

    public float speed;
    public int health;
    private int maxHealth;
    public bool isShieldActive = false;


    private void Start()
    {
        if (healthbar != null)
        {
            healthbar.maxValue = health;
            healthbar.value = health;
            maxHealth = health;
        }

        if (scoreText != null)
        {
            scoreText.SetText("score: " + score.ToString());
        }

    }
    private void Update()
    {
      
        if(health<=0)
        {
            SceneManager.LoadScene("DeathScene");
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
        if(isShieldActive==false)
        {
            health--;
        }
        else
        {
            health++;
        }
       
        healthbar.value = health;
        return health > 0;
    }

    public void Reset()
    {
        if (healthbar != null)
        {
            health = maxHealth;
            healthbar.value = health;
            score = 0;
        }

    }
}
