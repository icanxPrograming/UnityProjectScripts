using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameManager : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("HasLoadedGame", 0) == 1)
        {
            Invoke(nameof(ClearLoadFlag), 0.1f);
        }
    }

    void ClearLoadFlag()
    {
        PlayerPrefs.SetInt("HasLoadedGame", 0);
        PlayerPrefs.Save();
    }
}
