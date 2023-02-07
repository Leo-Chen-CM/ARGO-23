using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputName : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] private TMP_InputField m_nameInputField = null;
    [SerializeField] private Button m_continueButton = null;


    public static string DisplayName { get; private set; }

    private const string PlayerPrefsNameKey = "PlayerName";

    // Start is called before the first frame update
    void Start() => SetupInoutField();
    private void SetupInoutField()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsNameKey))
        {
            return;
        }

        string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

        m_nameInputField.text = defaultName;

        SetPlayerName(defaultName);
    }

    public void SetPlayerName(string t_name)
    {
        m_continueButton.interactable = !string.IsNullOrEmpty(t_name);
    }


    public void SavePlayerName()
    {
        DisplayName = m_nameInputField.text;

        PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);
    }
}
