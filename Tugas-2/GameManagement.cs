using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public TMP_Text Judul;

    public void PanelName(string NamaPanel)
    {
        Judul.SetText(NamaPanel);
    }

    public TMP_Text AudioTxt;
    public TMP_Text BgmTxt;

    public void ToggleAudio()
    {
        AudioTxt.text = (AudioTxt.text == "Audio : ON")
            ? "Audio : OFF"
            : "Audio : ON";
    }

    public void ToggleBgm()
    {
        BgmTxt.text = (BgmTxt.text == "BGM : ON")
            ? "BGM : OFF"
            : "BGM : ON";
    }

    public GameObject button;

    public void ShowButton()
    {
        button.SetActive(true);
    }

    public void BackToMainMenu()
    {
         SceneManager.LoadScene("SampleScene");
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
}
