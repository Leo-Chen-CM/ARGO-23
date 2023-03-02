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
        private Color m_originalColor;
        public GameObject m_player;
        public float speed;
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
        public void increaseHp()
        {
           m_player.GetComponent<SpriteRenderer>().material.color = m_originalColor;
            if (healthbar != null)
            {
                health = maxHealth;
                healthbar.value = health;
            
            }
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