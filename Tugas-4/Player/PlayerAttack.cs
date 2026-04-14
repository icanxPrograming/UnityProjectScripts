using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Skill Prefabs")]
    public GameObject iceBallPrefab;
    public GameObject fireBallPrefab;
    public GameObject lightningPrefab;

    [Header("Settings")]
    public float spawnOffset = 0.6f;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            UseSkill(iceBallPrefab);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            UseSkill(fireBallPrefab);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            UseLightning();
    }

    // ===================== PROJECTILE =====================
    void UseSkill(GameObject skillPrefab)
    {
        if (skillPrefab == null) return;

        Vector2 dirVector = GetFacingDirection();

        Debug.Log("[SKILL] Facing Vector: " + dirVector);
        Debug.DrawRay(transform.position, dirVector * 2f, Color.green, 1f);

        Vector3 spawnPos = transform.position + (Vector3)(dirVector * spawnOffset);

        GameObject skill = Instantiate(skillPrefab, spawnPos, Quaternion.identity);

        Projectile proj = skill.GetComponent<Projectile>();
        if (proj != null)
            proj.SetDirection(dirVector);
    }

    // ===================== LIGHTNING =====================
    void UseLightning()
    {
        if (lightningPrefab == null) return;

        Vector2 dirVector = GetFacingDirection();

        Debug.Log("[LIGHTNING] Facing Vector: " + dirVector);
        Debug.DrawRay(transform.position, dirVector * 2f, Color.red, 1f);

        Vector3 spawnPos = transform.position + (Vector3)(dirVector * 2f);

        GameObject lightning = Instantiate(lightningPrefab, spawnPos, Quaternion.identity);

        float angle = Mathf.Atan2(dirVector.y, dirVector.x) * Mathf.Rad2Deg;
        lightning.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // ===================== FIXED DIRECTION =====================
    Vector2 GetFacingDirection()
    {
        int dir = anim.GetInteger("Direction");

        switch (dir)
        {
            case 0: return Vector2.left;
            case 1: return Vector2.right;
            case 2: return Vector2.up;
            case 3: return Vector2.down;
        }

        return Vector2.right;
    }
}