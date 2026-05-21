using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PlayerPrefsManager
{
    // ================= COIN =================
    public static void SaveCoin(int coin)
    {
        PlayerPrefs.SetInt("Coin", coin);
    }

    public static int LoadCoin()
    {
        return PlayerPrefs.GetInt("Coin", 0);
    }

    // ================= PLAYER POSITION =================
    public static void SavePlayerPosition(Vector3 pos)
    {
        PlayerPrefs.SetFloat("PlayerX", pos.x);
        PlayerPrefs.SetFloat("PlayerY", pos.y);
    }

    public static Vector3 LoadPlayerPosition()
    {
        float x = PlayerPrefs.GetFloat("PlayerX", 0f);
        float y = PlayerPrefs.GetFloat("PlayerY", 0f);
        return new Vector3(x, y, 0f);
    }

    // ================= HEALTH =================
    public static void SaveHealth(float hpWidth, int heartIndex)
    {
        PlayerPrefs.SetFloat("HPWidth", hpWidth);
        PlayerPrefs.SetInt("HeartIndex", heartIndex);
    }

    public static float LoadHPWidth(float defaultWidth)
    {
        return PlayerPrefs.GetFloat("HPWidth", defaultWidth);
    }

    public static int LoadHeartIndex(int defaultIndex)
    {
        return PlayerPrefs.GetInt("HeartIndex", defaultIndex);
    }

    // ================= TIME =================
    public static void SaveTime()
    {
        PlayerPrefs.SetString("SaveTime", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
    }

    public static string LoadTime()
    {
        return PlayerPrefs.GetString("SaveTime", "-");
    }

    // ================= APPLY =================
    public static void SaveAll()
    {
        PlayerPrefs.Save();
        Debug.Log("GAME SAVED");
    }
}
