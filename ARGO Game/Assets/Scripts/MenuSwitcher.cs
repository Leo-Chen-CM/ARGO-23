using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;



public class MenuSwitcher : MonoBehaviour
{
   
    public void LoadCredits()
    {
        SceneManager.LoadScene("credits");
    }

  
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    
    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Loaddeath()
    {
        SceneManager.LoadScene("DeathScene");
    }


    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
