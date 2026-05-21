using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputNameManager : MonoBehaviour
{
    public TMP_InputField inputField;

    public void OnClickStart()
    {
        string name = inputField.text.Trim();

        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("Nama tidak boleh kosong!");
            return;
        }

        PlayerPrefs.SetInt("HasLoadedGame", 0);
        PlayerPrefs.DeleteKey("SelectedSlot");
        PlayerPrefs.Save();

        if (GameManagement.Instance != null)
        {
            GameManagement.Instance.playerName = name;
            GameManagement.Instance.isNewGame = true;
        }

        GameManagement.Instance.ScnNewGame();
    }

    public void OnClickBack()
    {
        GameManagement.Instance.ShowMainMenuPanel();
        GameManagement.Instance.BackToMainMenu();
    }
}