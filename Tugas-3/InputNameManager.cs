using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputNameManager : MonoBehaviour
{
    public TMP_InputField inputField;

    // Tombol Start
    public void OnClickStart()
    {
        string name = inputField.text;

        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("Nama tidak boleh kosong!");
            return;
        }

        GameManagement.Instance.playerName = name;

        GameManagement.Instance.ScnNewGame();
    }

    public void OnClickBack()
    {
        GameManagement.Instance.ShowMainMenuPanel();
        GameManagement.Instance.BackToMainMenu();
    }
}