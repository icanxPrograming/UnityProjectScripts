using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("HasLoadedGame", 0) != 1)
            return;

        int slot = PlayerPrefs.GetInt("SelectedSlot", 1);
        string key = $"Slot{slot}_";

        if (GameManagement.Instance != null)
        {
            GameManagement.Instance.playerName =
                PlayerPrefs.GetString(key + "Name", "Player");
        }

        // POSISI
        float x = PlayerPrefs.GetFloat(key + "PosX", transform.position.x);
        float y = PlayerPrefs.GetFloat(key + "PosY", transform.position.y);
        float z = PlayerPrefs.GetFloat(key + "PosZ", transform.position.z);
        transform.position = new Vector3(x, y, z);

        // COIN
        PlayerCoin coin = GetComponent<PlayerCoin>();
        if (coin != null)
            coin.SetCoin(PlayerPrefs.GetInt(key + "Coin", 0));

        // HEALTH
        PlayerHealth health = GetComponent<PlayerHealth>();
        if (health != null)
            health.LoadHealth(
                PlayerPrefs.GetFloat(key + "HpWidth", health.hpBarMaxWidth),
                PlayerPrefs.GetInt(key + "Heart", health.MaxHeart())
            );

        // RESET FLAG
        PlayerPrefs.SetInt("HasLoadedGame", 0);
        PlayerPrefs.Save();
    }
}
