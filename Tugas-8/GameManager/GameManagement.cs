using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance;

    [Header("Player Info")]
    public string playerName;

    [Header("UI Panels")]
    public GameObject PanelMainMenu;

    [Header("UI")]
    public TMP_Text Judul;
    public TMP_Text AudioTxt;
    public TMP_Text BgmTxt;
    public GameObject button;

    [Header("Audio/BGM State")]
    public bool isAudioOn = true;
    public bool isBgmOn = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnClickNewGame()
    {
        if (PanelMainMenu != null)
        {
            PanelMainMenu.SetActive(false);
        }

        ScnInputName();
    }

    public void OnClickContinue()
    {
        if (PanelMainMenu != null)
        {
            PanelMainMenu.SetActive(false);
        }

        ScnContinue();
    }

    public void ShowMainMenuPanel()
    {
        if (PanelMainMenu != null)
        {
            PanelMainMenu.SetActive(true);
        }
        else
        {
            Debug.LogWarning("PanelMainMenu belum diassign di inspector!");
        }
    }

    #region Scene Management
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ScnInputName()
    {
        SceneManager.LoadScene("InputNameScene");
    }

    public void ScnNewGame()
    {
        SceneManager.LoadScene("NewGameScene");
    }

    public void ScnContinue()
    {
        SceneManager.LoadScene("ContinueScene");
    }

    public void exit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    #endregion

    #region UI
    public void PanelName(string NamaPanel)
    {
        if (Judul != null)
            Judul.SetText(NamaPanel);
        else
            Debug.LogWarning("Judul TMP_Text belum diassign di inspector!");
    }

    public void ToggleAudio()
    {
        isAudioOn = !isAudioOn;
        if (AudioTxt != null)
            AudioTxt.text = isAudioOn ? "Audio : ON" : "Audio : OFF";
    }

    public void ToggleBgm()
    {
        isBgmOn = !isBgmOn;
        if (BgmTxt != null)
            BgmTxt.text = isBgmOn ? "BGM : ON" : "BGM : OFF";
    }

    public void ShowButton()
    {
        if (button != null)
            button.SetActive(true);
    }
    #endregion
}