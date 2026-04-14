using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    [System.Serializable]
    public class SkillSlot
    {
        public Image slotImage;
        public KeyCode key;
    }

    public SkillSlot[] slots;

    public Color normalColor = new Color(0.3f, 0.3f, 0.3f);
    public Color activeColor = Color.cyan;
    public float activeDuration = 0.2f;

    void Start()
    {
        foreach (var slot in slots)
        {
            slot.slotImage.color = normalColor;
        }
    }

    void Update()
    {
        foreach (var slot in slots)
        {
            if (Input.GetKeyDown(slot.key))
            {
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
        yield return new WaitForSeconds(activeDuration);
        img.color = normalColor;
    }
}
