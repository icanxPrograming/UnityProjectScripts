using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveLoader : MonoBehaviour
{
    void Start()
    {
        Vector3 savedPos = PlayerPrefsManager.LoadPlayerPosition();
        transform.position = savedPos;
    }

    void OnApplicationQuit()
    {
        SavePosition();
    }

    public void SavePosition()
    {
        PlayerPrefsManager.SavePlayerPosition(transform.position);
    }
}
