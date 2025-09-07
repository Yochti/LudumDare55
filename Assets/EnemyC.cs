using UnityEngine;

public class EnemyC : MonoBehaviour
{
    [Header("Vitesse")]
    public float originalSpeed = 2f;
    public float currentSpeed;

    [Header("Stun")]
    public float stunCooldown = 2f; // Temps entre deux stuns
    private bool isStunned = false;
    private float stunTimer = 0f;
    private float stunCooldownTimer = 0f;

    [Header("Slow")]
    private bool isSlowed = false;
    private float slowFactor = 0.7f;

    void Start()
    {
        currentSpeed = originalSpeed;
    }

    void Update()
    {
        if (stunCooldownTimer > 0f)
            stunCooldownTimer -= Time.deltaTime;

        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                isStunned = false;
                stunCooldownTimer = stunCooldown;
                ResetSpeed();
            }

            return; 
        }


    }



    public void Stun(float duration)
    {
        if (stunCooldownTimer <= 0f)
        {
            isStunned = true;
            stunTimer = duration;
            currentSpeed = 0f;
            isSlowed = false;
        }
    }

    public void ApplySlow(float factor)
    {
        if (!isStunned)
        {
            isSlowed = true;
            slowFactor = factor;
            currentSpeed = originalSpeed * slowFactor;
        }
    }

    public void ResetSpeed()
    {
        if (!isStunned)
        {
            currentSpeed = originalSpeed;
            isSlowed = false;
        }
    }

    public bool IsStunned => isStunned;
    public bool IsSlowed => isSlowed;
    public bool CanBeStunned => stunCooldownTimer <= 0f;
}
