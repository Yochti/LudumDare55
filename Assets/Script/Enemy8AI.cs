using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy8AI : MonoBehaviour
{
    [Header("Fusion Settings")]
    public float detectionRadius = 10f;
    public GameObject largeGolemPrefab;
    public float fusionDistance = 1.5f;
    public float fusionCheckInterval = 0.5f;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float attackRange = 3.5f;

    [Header("Combat Settings")]
    public int damage = 10;
    public float attackCooldown = 1f;

    private float nextAttackTime = 0f;
    private float nextFusionCheckTime = 0f;

    private bool isMerging = false;
    private Transform player;
    private Rigidbody2D rb;
    private EnemyC enemyC;
    private GameObject fusionTarget;

    private void Start()
    {
        enemyC = GetComponent<EnemyC>();
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    private void Update()
    {
        if (isMerging || player == null) return;

        if (Time.time >= nextFusionCheckTime)
        {
            fusionTarget = FindClosestGolem();
            nextFusionCheckTime = Time.time + fusionCheckInterval;
        }

        if (fusionTarget != null)
        {
            float sqrDist = ((Vector2)fusionTarget.transform.position - (Vector2)transform.position).sqrMagnitude;
            if (sqrDist <= fusionDistance * fusionDistance)
            {
                FuseWithGolem(fusionTarget);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isMerging || player == null) return;

        moveSpeed = enemyC.currentSpeed;

        if (fusionTarget != null)
        {
            MoveTowards(fusionTarget.transform.position);
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    private GameObject FindClosestGolem()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        GameObject closest = null;
        float minSqrDist = Mathf.Infinity;
        Vector2 currentPos = transform.position;

        foreach (Collider2D col in hits)
        {
            if (col.gameObject == gameObject) continue;

            Enemy8AI other = col.GetComponent<Enemy8AI>();
            if (other != null && !other.isMerging)
            {
                float sqrDist = ((Vector2)col.transform.position - currentPos).sqrMagnitude;
                if (sqrDist < minSqrDist)
                {
                    minSqrDist = sqrDist;
                    closest = col.gameObject;
                }
            }
        }

        return closest;
    }

    private void MoveTowards(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        float sqrDist = ((Vector2)player.position - (Vector2)transform.position).sqrMagnitude;
        if (sqrDist <= attackRange * attackRange)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        if (Time.time >= nextAttackTime)
        {
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    private void FuseWithGolem(GameObject otherGolem)
    {
        if (otherGolem == null) return;

        isMerging = true;
        Vector2 fusionPos = (transform.position + otherGolem.transform.position) * 0.5f;
        Instantiate(largeGolemPrefab, fusionPos, Quaternion.identity);

        Destroy(otherGolem);
        Destroy(gameObject);
    }
}
