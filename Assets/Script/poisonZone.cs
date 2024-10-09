using UnityEngine;

public class PoisonZone : MonoBehaviour
{
    public float damage = 15f;
    public float cooldown = 0.4f;
    public float lifetime = 15f;

    private bool canDamage = true;

    private void Start()
    {
        Invoke("DestroyGO", lifetime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canDamage && collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                canDamage = false;
                Invoke("ResetDamageCooldown", cooldown);
            }
        }
    }

    private void ResetDamageCooldown()
    {
        canDamage = true;
    }

    private void DestroyGO()
    {
        Destroy(gameObject);
    }
}
