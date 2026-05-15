using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Skill Prefabs")]
    public GameObject iceBallPrefab;
    public GameObject fireBallPrefab;
    public GameObject lightningPrefab;

    [Header("Cooldown (seconds)")]
    public float iceCooldown = 1.5f;
    public float fireCooldown = 2f;
    public float lightningCooldown = 4f;

    [Header("Settings")]
    public float spawnOffset = 0.6f;

    Animator anim;

    float iceLastTime;
    float fireLastTime;
    float lightningLastTime;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            TryUseSkill(iceBallPrefab, iceCooldown, ref iceLastTime);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            TryUseSkill(fireBallPrefab, fireCooldown, ref fireLastTime);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            TryUseLightning();
    }

    // ================= PROJECTILE =================
    void TryUseSkill(GameObject prefab, float cooldown, ref float lastTime)
    {
        if (Time.time < lastTime + cooldown)
            return;

        lastTime = Time.time;
        UseSkill(prefab);
    }

    void UseSkill(GameObject skillPrefab)
    {
        if (skillPrefab == null) return;

        Vector2 dirVector = GetFacingDirection();
        Vector3 spawnPos = transform.position + (Vector3)(dirVector * spawnOffset);

        GameObject skill = Instantiate(skillPrefab, spawnPos, Quaternion.identity);

        Projectile proj = skill.GetComponent<Projectile>();
        if (proj != null)
            proj.SetDirection(dirVector);
    }

    // ================= LIGHTNING ULTIMATE =================
    void TryUseLightning()
    {
        if (Time.time < lightningLastTime + lightningCooldown)
            return;

        lightningLastTime = Time.time;

        Vector2[] directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

        float ultimateRange = 2.5f;

        foreach (Vector2 dir in directions)
        {
            Vector3 spawnPos = transform.position + (Vector3)(dir * ultimateRange);

            GameObject lightning = Instantiate(lightningPrefab, spawnPos, Quaternion.identity);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            lightning.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public float GetCooldownRemaining(int index)
    {
        return index switch
        {
            0 => Mathf.Max(0, iceLastTime + iceCooldown - Time.time),
            1 => Mathf.Max(0, fireLastTime + fireCooldown - Time.time),
            2 => Mathf.Max(0, lightningLastTime + lightningCooldown - Time.time),
            _ => 0
        };
    }

    // ================= DIRECTION =================
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

    public bool IsIceReady() => Time.time >= iceLastTime + iceCooldown;
    public bool IsFireReady() => Time.time >= fireLastTime + fireCooldown;
    public bool IsLightningReady() => Time.time >= lightningLastTime + lightningCooldown;
}