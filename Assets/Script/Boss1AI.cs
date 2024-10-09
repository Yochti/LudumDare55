using UnityEngine;

public class ElectricSentinel : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float damageRadius = 1.5f;
    public float damagePerSecond = 60f;
    public float damageCooldown = 1f;
    private Transform player;
    private PlayerHealth playerHealth;
    private bool canDamage = true;
    private Boss1Health enemyHealth; 
    private EnemyWaveManager waveManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<Boss1Health>(); 
        waveManager = FindObjectOfType<EnemyWaveManager>(); // Récupération de l'instance d'EnemyWaveManager dans la scène
        AdjustHealth(); // Appel de la fonction pour ajuster les points de vie de l'Electric Sentinel
    }

    void Update()
    {
        if (player != null)
        {
            MoveTowardsPlayer();

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < damageRadius && canDamage)
            {
                DamagePlayer();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    void DamagePlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(Mathf.RoundToInt(damagePerSecond));
        }

        canDamage = false;
        Invoke("ResetDamage", damageCooldown);
    }

    void ResetDamage()
    {
        canDamage = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canDamage)
        {
            DamagePlayer();
        }
    }

    void AdjustHealth()
    {
        int currentWave = waveManager.currentWave; 
        int modifiedHealth = enemyHealth.maxHealth + (currentWave * 250);
        enemyHealth.currentHealth = modifiedHealth;
        enemyHealth.maxHealth= modifiedHealth;
    }
}
