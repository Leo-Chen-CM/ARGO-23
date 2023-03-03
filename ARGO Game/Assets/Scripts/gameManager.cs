using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    private int score = 0;
    /// the text that displays the score
    public TMP_Text scoreText;
    /// the bar that displays the health
    public Slider healthbar;
    private Color m_originalColor;
    /// reference to the player
    public GameObject m_player;
    /// speed that the game moves at
    public float speed;
    /// the players remaining health
    public int health;
    private int maxHealth;


    private void Start()
    {
        if (healthbar != null)
        {
            healthbar.maxValue = health;
            healthbar.value = health;
            maxHealth = health;
        }
        m_originalColor= m_player.gameObject.GetComponent<SpriteRenderer>().sharedMaterial.color;
        if (scoreText != null)
        {
            scoreText.SetText("score: " + score.ToString());
        }
    }
    private void Update()
    {
      
        if(health<=0)
        {
            FindObjectOfType<Mirror.Examples.Basic.NewNetworkRoomManager>().StopClient();
            FindObjectOfType<Mirror.Examples.Basic.NewNetworkRoomManager>().StopHost();
            Destroy(FindObjectOfType<Mirror.Examples.Basic.NewNetworkRoomManager>().gameObject);
            SceneManager.LoadScene("DeathScene");
        }
    }

    /// <summary>
    /// Increments the score
    /// </summary>
    public void addScore()
    {
        if (scoreText != null)
        {
            score++;
            scoreText.SetText("score: " + score.ToString());
        }
    }

    /// <summary>
    /// returns the speed value
    /// </summary>
    /// <returns></returns>
    public float getSpeed()
    {
        return speed;
    }

    /// <summary>
    /// decrements health
    /// </summary>
    /// <returns>whether health is at 0</returns>
    public bool reduceHealth()
    {
        health--;
        healthbar.value = health;
        return health > 0;
    }

    /// <summary>
    /// increments the health
    /// </summary>
    public void increaseHp()
    {
        if (healthbar != null)
        {
            AudioManager.Instance().PlaySoundEffect(AudioManager.SoundEffect.Shield);
            health = maxHealth;
            healthbar.value = health;
            
        }
    }

    /// <summary>
    /// Resets the UI for a new game
    /// </summary>
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