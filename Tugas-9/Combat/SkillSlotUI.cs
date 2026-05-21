using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    [System.Serializable]
    public class SkillSlot
    {
        public Image slotImage;
        public Text cooldownText;
        public KeyCode key;
        public int skillIndex;
    }

    public SkillSlot[] slots;
    public PlayerAttack playerAttack;

    public Color normalColor = new Color(0.3f, 0.3f, 0.3f);
    public Color activeColor = Color.cyan;
    public Color cooldownColor = Color.gray;

    void Start()
    {
        foreach (var slot in slots)
        {
            slot.slotImage.color = normalColor;
            slot.cooldownText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        foreach (var slot in slots)
        {
            float remain = playerAttack.GetCooldownRemaining(slot.skillIndex);

            if (remain > 0)
            {
                slot.slotImage.color = cooldownColor;
                slot.cooldownText.gameObject.SetActive(true);
                slot.cooldownText.text = Mathf.Ceil(remain).ToString();
            }
            else
            {
                slot.cooldownText.gameObject.SetActive(false);
                slot.slotImage.color = normalColor;

                if (Input.GetKeyDown(slot.key))
                    ActivateSlot(slot.slotImage);
            }
        }
    }

    void ActivateSlot(Image img)
    {
        img.color = activeColor;
        StartCoroutine(ResetColor(img));
    }

    IEnumerator ResetColor(Image img)
    {
        yield return new WaitForSeconds(0.15f);
        img.color = normalColor;
    }
}