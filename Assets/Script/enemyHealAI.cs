using UnityEngine;

public class HealingEnemy : MonoBehaviour
{
    [Header("Detection Settings")]
    public float detectionRadius = 200f; // Rayon de détection des ennemis
    public float healingRange = 2f; // Portée de guérison

    [Header("Healing Settings")]
    public float healingRate = 8f; // Taux de guérison par seconde
    public int healAmount = 3; // Quantité de points de vie guéris par tick

    [Header("Movement Settings")]
    public float moveSpeed = 3f; // Vitesse de déplacement

    private Transform targetPlayer;
    private float healCooldown;

    void Update()
    {
        FindPlayer();
        MoveTowardsPlayer();

        if (Time.time >= healCooldown)
        {
            HealEnemiesInRange();
            healCooldown = Time.time + 1f / healingRate;
        }
    }

    void FindPlayer()
    {
        // Si un joueur est déjà ciblé, évitez de recalculer
        if (targetPlayer != null) return;

        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D collider in objectsInRange)
        {
            PlayerMovement player = collider.GetComponent<PlayerMovement>();
            if (player != null)
            {
                targetPlayer = player.transform;
                break;
            }
        }
    }

    void MoveTowardsPlayer()
    {
        if (targetPlayer == null) return;

        Vector2 direction = ((Vector2)targetPlayer.position - (Vector2)transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void HealEnemiesInRange()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, healingRange);

        foreach (Collider2D collider in enemiesInRange)
        {
            EnemmiHealth enemyHealth = collider.GetComponent<EnemmiHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.Heal(healAmount);
            }
        }
    }


}
