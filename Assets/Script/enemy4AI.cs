using UnityEngine;

public class SlimeAcid : MonoBehaviour
{
    public GameObject smallerSlimePrefab; // Prefab du slime plus petit
    public float moveSpeed = 2f; // Vitesse de déplacement du slime
    public int splitCount = 2; // Nombre de slimes créés lorsque celui-ci est vaincu
    public float attackRange = 1.5f; // Portée de l'attaque
    public float attackCooldown = 2f; // Temps de recharge entre chaque attaque
    public float attackDamage = 10f; // Dégâts infligés au joueur lors de l'attaque
    public EnemmiHealth enemyHp; // Référence à la santé de l'ennemi
    private Transform player; // Référence au joueur
    private Transform poutch; // Référence au poutch
    private bool canAttack = true; // Indique si le slime peut attaquer
    private bool isDead = false; // Indique si le slime est mort
    private Vector3 targetPosition; // Dernière position connue de la cible
    public float detectionRadius = 8f; // Rayon de détection
    private EnemyC enemyC;

    void Start()
    {
        enemyC = this.gameObject.GetComponent<EnemyC>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        moveSpeed = enemyC.currentSpeed;

        if (poutch == null)
        {
            GameObject poutchObject = GameObject.FindGameObjectWithTag("Poutch");
            if (poutchObject != null)
            {
                poutch = poutchObject.transform;
            }
        }

        if (!isDead)
        {
            if (poutch != null)
            {
                MoveTowardsTarget(poutch);
            }
            else if (player != null && !PlayerInvisibleZone.isInvisible)
            {
                MoveTowardsTarget(player);
                if (Vector2.Distance(transform.position, player.position) <= attackRange && canAttack)
                {
                    Attack(player);
                }
            }
            else if (player != null)
            {
                MoveTowardsApproximatePosition();
                if (Vector2.Distance(transform.position, player.position) <= attackRange && canAttack)
                {
                    Attack(player);
                }
            }

            if (enemyHp.currentHealth <= 0)
            {
                isDead = true;
                SpawnSmallerSlimes();
                Destroy(gameObject);
            }
        }
    }

    private void MoveTowardsTarget(Transform target)
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
        for (int i = 0; i < splitCount; i++)
        {
            Instantiate(smallerSlimePrefab, transform.position, Quaternion.identity);
        }
    }

    void Attack(Transform target)
    {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
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
