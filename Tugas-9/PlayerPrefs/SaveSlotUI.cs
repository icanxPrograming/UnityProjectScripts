using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SaveSlotUI : MonoBehaviour
{
    public enum SlotMode { Save, Load }
    public SlotMode mode;

    [Header("Slot Settings")]
    public int slotIndex;

    [Header("UI")]
    public GameObject emptySlot;
    public TMP_Text slotNameText;
    public TMP_Text infoText;
    public TMP_Text timeText;
    public GameObject deleteButton;

    void Start()
    {
        LoadSlotUI();
    }

    public void OnClickSlot()
    {
        if (mode == SlotMode.Save)
        {
            SaveGameToSlot();
            LoadSlotUI();
        }
        else
        {
            LoadGameFromSlot();
        }
    }

    // ================= SAVE =================
    void SaveGameToSlot()
    {
        string key = $"Slot{slotIndex}_";

        string playerName = GameManagement.Instance.playerName;
        PlayerPrefs.SetString(key + "Name", playerName);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerHealth health = FindObjectOfType<PlayerHealth>();
        PlayerCoin coin = FindObjectOfType<PlayerCoin>();

        if (player != null)
        {
            Vector3 pos = player.transform.position;
            PlayerPrefs.SetFloat(key + "PosX", pos.x);
            PlayerPrefs.SetFloat(key + "PosY", pos.y);
            PlayerPrefs.SetFloat(key + "PosZ", pos.z);
        }

        if (coin != null)
            PlayerPrefs.SetInt(key + "Coin", coin.coin);

        if (health != null)
        {
            PlayerPrefs.SetFloat(key + "HpWidth", health.GetCurrentHpWidth());
            PlayerPrefs.SetInt(key + "Heart", health.GetCurrentHeart());
        }

        PlayerPrefs.SetString(key + "Time", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
        PlayerPrefs.SetInt(key + "Used", 1);
        PlayerPrefs.Save();
    }

    // ================= LOAD =================
    void LoadGameFromSlot()
    {
        string key = $"Slot{slotIndex}_";

        if (PlayerPrefs.GetInt(key + "Used", 0) != 1)
            return;

        if (GameManagement.Instance != null)
        {
            GameManagement.Instance.playerName =
                PlayerPrefs.GetString(key + "Name", "Player");
        }

        PlayerPrefs.SetInt("SelectedSlot", slotIndex);
        PlayerPrefs.SetInt("HasLoadedGame", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene("NewGameScene");
    }

    // ================= UI =================
    void LoadSlotUI()
    {
        string key = $"Slot{slotIndex}_";
        bool used = PlayerPrefs.GetInt(key + "Used", 0) == 1;

        emptySlot.SetActive(!used);
        slotNameText.gameObject.SetActive(used);
        infoText.gameObject.SetActive(used);
        timeText.gameObject.SetActive(used);
        if (deleteButton != null)
            deleteButton.SetActive(used);

        if (!used) return;

        slotNameText.text =
            $"Slot {slotIndex} - {PlayerPrefs.GetString(key + "Name", "Player")}";
        infoText.text = "Coin : " + PlayerPrefs.GetInt(key + "Coin", 0);
        timeText.text = PlayerPrefs.GetString(key + "Time", "-");
    }

    public void DeleteSlot()
    {
        string key = $"Slot{slotIndex}_";

        PlayerPrefs.DeleteKey(key + "Name");
        PlayerPrefs.DeleteKey(key + "PosX");
        PlayerPrefs.DeleteKey(key + "PosY");
        PlayerPrefs.DeleteKey(key + "PosZ");
        PlayerPrefs.DeleteKey(key + "Coin");
        PlayerPrefs.DeleteKey(key + "HpWidth");
        PlayerPrefs.DeleteKey(key + "Heart");
        PlayerPrefs.DeleteKey(key + "Time");
        PlayerPrefs.DeleteKey(key + "Used");

        PlayerPrefs.Save();
        LoadSlotUI();
    }
}