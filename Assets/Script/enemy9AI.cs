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
    public float detectionRadius = 8f;
    private Vector3 targetPosition;
    private Transform poutch;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!isDead && player != null)
        {
            if (poutch == null)
            {
                GameObject poutchObject = GameObject.FindGameObjectWithTag("Poutch");
                if (poutchObject != null)
                {
                    poutch = poutchObject.transform;
                }
            }
            if(poutch != null)
            {
                MoveTowardsPlayer(poutch);
            }
            else if (!PlayerInvisibleZone.isInvisible)
                MoveTowardsPlayer(player);
            else
                MoveTowardsApproximatePosition();
            

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

    void MoveTowardsPlayer(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }


    private void MoveTowardsApproximatePosition()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.5f || targetPosition == Vector3.zero)
        {
            targetPosition = GetRandomPointAround(PlayerInvisibleZone.lastKnownPosition);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);  
    }

    private Vector3 GetRandomPointAround(Vector3 center)
    {
        Vector2 randomPoint = Random.insideUnitCircle * detectionRadius;
        return new Vector3(center.x + randomPoint.x, center.y + randomPoint.y, transform.position.z);
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
