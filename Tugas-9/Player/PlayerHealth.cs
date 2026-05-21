using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("HP Bar")]
    public Image hpBarImage;
    public float hpBarMaxWidth = 74.9949f;
    public float hpReducePerHit = 5f;

    [Header("Hearts (urut dari H1 ? H3)")]
    public Image[] hearts;

    [Header("Collision")]
    public string enemyTag = "Enemy";

    [Header("Damage Settings")]
    public float damageInterval = 0.5f;

    float lastDamageTime;

    float currentHpWidth;
    int currentHeartIndex;

    public GameOverUI gameOverUI;

    void Start()
    {
        currentHpWidth = PlayerPrefsManager.LoadHPWidth(hpBarMaxWidth);
        currentHeartIndex = PlayerPrefsManager.LoadHeartIndex(hearts.Length - 1);

        for (int i = 0; i < hearts.Length; i++)
            hearts[i].gameObject.SetActive(i <= currentHeartIndex);

        SetHpBarWidth(currentHpWidth);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(enemyTag))
            return;

        if (Time.time >= lastDamageTime + damageInterval)
        {
            lastDamageTime = Time.time;
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        currentHpWidth -= hpReducePerHit;

        if (currentHpWidth <= 0)
        {
            LoseHeart();
        }
        else
        {
            SetHpBarWidth(currentHpWidth);
        }
    }

    void LoseHeart()
    {
        if (currentHeartIndex < 0)
        {
            Debug.Log("PLAYER DEAD");

            gameOverUI.ShowGameOver();
            return;
        }

        hearts[currentHeartIndex].gameObject.SetActive(false);
        currentHeartIndex--;

        currentHpWidth = hpBarMaxWidth;
        SetHpBarWidth(currentHpWidth);
    }

    void SetHpBarWidth(float width)
    {
        RectTransform rt = hpBarImage.rectTransform;
        rt.sizeDelta = new Vector2(width, rt.sizeDelta.y);
    }

    public void RestoreHealth(float amount)
    {
        if (currentHeartIndex < hearts.Length - 1)
        {
            currentHpWidth += amount;
            if (currentHpWidth >= hpBarMaxWidth)
            {
                currentHpWidth = hpBarMaxWidth;
                SetHpBarWidth(currentHpWidth);

                RestoreHeart();
            }
            else
            {
                SetHpBarWidth(currentHpWidth);
            }
        }
    }

    public void SaveHealthData()
    {
        PlayerPrefsManager.SaveHealth(currentHpWidth, currentHeartIndex);
    }

    void RestoreHeart()
    {
        int nextHeart = currentHeartIndex + 1;
        if (nextHeart >= hearts.Length) return;

        hearts[nextHeart].gameObject.SetActive(true);
        currentHeartIndex++;
    }

    public float GetCurrentHpWidth()
    {
        return currentHpWidth;
    }

    public int GetCurrentHeart()
    {
        return currentHeartIndex + 1;
    }

    public int MaxHeart() => hearts.Length;

    public void LoadHealth(float hpWidth, int heartCount)
    {
        currentHpWidth = hpWidth;
        SetHpBarWidth(currentHpWidth);

        for (int i = 0; i < hearts.Length; i++)
            hearts[i].gameObject.SetActive(i < heartCount);

        currentHeartIndex = heartCount - 1;
    }
}
