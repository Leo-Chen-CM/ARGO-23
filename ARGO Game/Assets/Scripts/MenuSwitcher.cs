using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;
using Mirror.Examples.MultipleMatch;

public class MenuSwitcher : MonoBehaviour
{
    /// <summary>
    /// Starts in game music
    /// </summary>
    private void Start()
    {
        AudioManager.Instance().PlayMusic(AudioManager.Music.Game);
    }

    /// <summary>
    /// Loads the Multiplayer Menu screen
    /// </summary>
    public void LoadMultiplayerMenu()
    {
        SceneManager.LoadScene("MultiplayerMenu");
    }

    /// <summary>
    /// Loads the Credit screen
    /// </summary>
    public void LoadCredits()
    {
        SceneManager.LoadScene("credits");
    }

    /// <summary>
    /// Loads the Game screen
    /// </summary>
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Loads the Settings screen
    /// </summary>
    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    /// <summary>
    /// Loads the Death screen
    /// </summary>
    public void Loaddeath()
    {
        SceneManager.LoadScene("DeathScene");
    }

    /// <summary>
    /// Loads the Menu screen
    /// </summary>
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("You quit the game");
    }
}
