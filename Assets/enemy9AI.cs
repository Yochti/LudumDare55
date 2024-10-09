using UnityEngine;

public class Enemy9AI : MonoBehaviour
{
    public GameObject smallerSlimePrefab; // Prefab du slime plus petit
    public float moveSpeed = 4f; // Vitesse de déplacement du slime
    public int splitCount = 2; // Nombre de slimes créés lorsque celui-ci est vaincu
    public float attackRange = 1.5f; // Portée de l'attaque
    public float attackCooldown = 2f; // Temps de recharge entre chaque attaque
    public float attackDamage = 10f; // Dégâts infligés au joueur lors de l'attaque
    public float fragmentSpawnDistance = 3f; 
    public EnemmiHealth enemyHp;
    private Transform player; // Référence au joueur
    private bool canAttack = true; // Indique si le slime peut attaquer
    private bool isDead = false; // Indique si le slime est mort

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!isDead && player != null)
        {
            MoveTowardsPlayer();

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange && canAttack)
            {
                AttackPlayer();
            }
            if (enemyHp.currentHealth <= 0)
            {
                isDead = true;
                SpawnSmallerSlimes();
                Destroy(gameObject);
            }
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }



    void SpawnSmallerSlimes()
    {

        for (int i = 0; i < 4; i++)
        {
            // Calculer une position aléatoire autour de l'ennemi
            Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle.normalized * fragmentSpawnDistance;
            Instantiate(smallerSlimePrefab, spawnPosition, Quaternion.identity);
        }

    }

    void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(Mathf.RoundToInt(attackDamage));
        }

        canAttack = false;
        Invoke("ResetAttack", attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
    }
}
