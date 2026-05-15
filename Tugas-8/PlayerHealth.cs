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
        currentHpWidth = hpBarMaxWidth;
        currentHeartIndex = hearts.Length - 1;
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
}
