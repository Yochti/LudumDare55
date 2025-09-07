using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    public float damage = 10f;
    public float detectionRadius = 7f;

    private Vector3 targetPosition;
    private Transform player;
    private Rigidbody2D rb;
    private bool canAttack = true;
    private Transform poutch;
    private EnemyC enemyC;

    void Start()
    {
        enemyC = this.gameObject.GetComponent<EnemyC>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
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

        if (poutch != null)
        {
            Vector2 direction = (poutch.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else if (player != null)
        {
            if (!PlayerInvisibleZone.isInvisible)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.velocity = direction * moveSpeed;

                float distanceToPlayer = Vector2.Distance(transform.position, player.position);
                if (distanceToPlayer <= attackRange && canAttack)
                {
                    AttackPlayer();
                }
            }
            else
            {
                MoveTowardsApproximatePosition();

                float distanceToPlayer = Vector2.Distance(transform.position, player.position);
                if (distanceToPlayer <= attackRange && canAttack)
                {
                    AttackPlayer();
                }
            }
        }
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

    void AttackPlayer()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
        canAttack = false;
        Invoke("ResetAttack", attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
    }
}
