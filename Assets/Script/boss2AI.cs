using UnityEngine;

public class AbyssalAssassin : MonoBehaviour
{
    public float movementSpeed = 6f;
    public float teleportCooldown = 5f;
    public float teleportRange = 10f;
    public float attackDamage = 30f;
    public float doubleDamageMultiplier = 2f;
    public float doubleDamageWindow = 2f;
    public float teleportOffset = 3.5f;
    private Transform player;
    private Rigidbody2D rb;
    private bool canTeleport = true;
    private bool justTeleported = false;
    private float lastTeleportTime;
    private Boss1Health bossHp;
    private EnemyWaveManager waveManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        bossHp = GetComponent<Boss1Health>(); 
        waveManager = FindObjectOfType<EnemyWaveManager>(); // Récupération de l'instance d'EnemyWaveManager dans la scène
    }

    void Update()
    {
        teleportCooldown -= Time.deltaTime;
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * movementSpeed;

            if (canTeleport)
            {
                TeleportToPlayer();
            }
        }
    }

    void TeleportToPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > teleportRange && teleportCooldown <= 0 )
        {
            Vector2 playerDirection = (player.position - transform.position).normalized;
            Vector2 teleportPosition = (Vector2)player.position + playerDirection * (teleportRange + teleportOffset);

            transform.position = teleportPosition;

            canTeleport = false;
            justTeleported = true;
            lastTeleportTime = Time.time;
            ResetTeleportCooldown();
        }
    }

    void ResetTeleportCooldown()
    {
        canTeleport = true;
        teleportCooldown = 5f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (justTeleported && (Time.time - lastTeleportTime) < doubleDamageWindow)
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage * doubleDamageMultiplier);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            }

            justTeleported = false;
        }
    }

    
}
