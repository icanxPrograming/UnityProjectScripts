using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueScene : MonoBehaviour
{
    void Start()
    {
        if (GameManagement.Instance != null && GameManagement.Instance.PanelMainMenu != null)
        {
            GameManagement.Instance.PanelMainMenu.SetActive(false);
        }
    }

    public void OnClickBack()
    {
        if (GameManagement.Instance != null)
        {
            GameManagement.Instance.ShowMainMenuPanel();
            GameManagement.Instance.BackToMainMenu();
        }
    }
}
