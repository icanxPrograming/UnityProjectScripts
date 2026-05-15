using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameScene : MonoBehaviour
{
    [Header("Player Info")]
    public Text Name;

    [Header("Settings Panel")]
    public GameObject settingsPanel;

    void Start()
    {
        if (Name != null && GameManagement.Instance != null)
        {
            Name.text = GameManagement.Instance.playerName;
        }

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    #region Settings Panel Functions

    public void OnClickSettings()
    {
        if (settingsPanel == null) return;

        bool isActive = settingsPanel.activeSelf;
        settingsPanel.SetActive(!isActive);

        Time.timeScale = isActive ? 1f : 0f;
    }

    public void OnClickQuit()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        Time.timeScale = 1f;

        if (GameManagement.Instance != null)
            GameManagement.Instance.ShowMainMenuPanel();

        // Kembali ke Main Menu
        GameManagement.Instance.BackToMainMenu();
    }

    #endregion
}