using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoin : MonoBehaviour
{
    public int coin;
    public Text coinText;

    void Start()
    {
        if (PlayerPrefs.GetInt("HasLoadedGame", 0) == 1)
        {
            coin = PlayerPrefs.GetInt("Coin", 0);
        }
        else
        {
            coin = 0;
        }

        UpdateUI();
    }

    public void AddCoin(int amount)
    {
        coin += amount;
        PlayerPrefsManager.SaveCoin(coin);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (coinText != null)
            coinText.text = coin.ToString();
    }

    public void SetCoin(int value)
    {
        coin = value;
        UpdateUI();
    }
}
