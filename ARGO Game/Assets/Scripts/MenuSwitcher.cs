using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;
using Mirror.Examples.MultipleMatch;

public class MenuSwitcher : MonoBehaviour
{
    public void LoadMultiplayerMenu()
    {
        SceneManager.LoadScene("MultiplayerMenu");
    }
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

    
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("You quit the game");
    }

}
