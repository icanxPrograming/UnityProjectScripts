using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    [Header("Panels")]
    public GameObject saveBarPanel;
    public GameObject settingsPanel;

    void Start()
    {
        if (saveBarPanel != null)
            saveBarPanel.SetActive(false);
    }

    public void OpenSaveBar()
    {
        if (saveBarPanel == null) return;

        if (saveBarPanel.activeSelf) return;

        StartCoroutine(OpenSaveBarRoutine());
    }

    IEnumerator OpenSaveBarRoutine()
    {
        yield return null;

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        saveBarPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void CloseSaveBar()
    {
        if (saveBarPanel != null)
            saveBarPanel.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public bool IsSaveBarOpen()
    {
        return saveBarPanel != null && saveBarPanel.activeSelf;
    }

    public void SaveGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerHealth health = FindObjectOfType<PlayerHealth>();

        if (player != null)
            PlayerPrefsManager.SavePlayerPosition(player.transform.position);

        if (health != null)
            health.SaveHealthData();

        PlayerPrefsManager.SaveTime();
        PlayerPrefsManager.SaveAll();

        Debug.Log("Game Saved!");
    }
}