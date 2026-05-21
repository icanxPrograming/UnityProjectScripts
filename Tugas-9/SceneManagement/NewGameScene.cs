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

    [Header("Save Panel")]
    public Save saveManager;

    bool isPaused = false;

    void Start()
    {
        if (Name != null && GameManagement.Instance != null)
        {
            Name.text = GameManagement.Instance.playerName;
        }

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleEscape();
        }
    }

    void HandleEscape()
    {
        if (saveManager != null && saveManager.IsSaveBarOpen())
        {
            saveManager.CloseSaveBar();
            return;
        }

        if (isPaused)
        {
            CloseSettings();
            return;
        }

        OpenSettings();
    }

    void OpenSettings()
    {
        if (settingsPanel == null) return;

        isPaused = true;
        settingsPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void CloseSettings()
    {
        if (settingsPanel == null) return;

        isPaused = false;
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    #region Pause Management

    void TogglePause()
    {
        if (settingsPanel == null) return;

        isPaused = !isPaused;
        settingsPanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void OnClickSettings()
    {
        if (isPaused)
            CloseSettings();
        else
            OpenSettings();
    }

    public void OnClickSave()
    {
        if (saveManager == null) return;

        saveManager.OpenSaveBar();
    }

    public void OnClickQuit()
    {
        Time.timeScale = 1f;
        isPaused = false;

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        if (GameManagement.Instance != null)
        {
            GameManagement.Instance.ShowMainMenuPanel();
            GameManagement.Instance.BackToMainMenu();
        }
    }

    #endregion
}