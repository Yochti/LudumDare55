using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    private Transform player;          // Référence vers le joueur
    private Transform poutch;          // Référence vers le poutch
    public GameObject bulletPrefab;    // Préfabriqué de la balle
    public float moveSpeed = 5f;       // Vitesse de déplacement de l'ennemi
    public float fireRate = 2f;        // Cadence de tir en secondes
    public float bulletSpeed = 10f;    // Vitesse de la balle
    private Vector3 targetPosition;    // Position vers laquelle l'ennemi se dirige
    private float nextFireTime;        // Temps avant le prochain tir
    public float detectionRadius = 7f; // Rayon autour de la dernière position connue du joueur

    private void Start()
    {
        // Initialiser la référence du joueur si présent
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        // Vérifier et mettre à jour les références du joueur et du poutch
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

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
            MoveTowardsTarget(poutch);
        }
        else if (player != null && !PlayerInvisibleZone.isInvisible)
        {
            MoveTowardsTarget(player);

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else if (player != null)
        {
            MoveTowardsApproximatePosition();
        }
    }

    private void MoveTowardsTarget(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        Vector2 direction = target.position - transform.position;
        transform.up = direction.normalized;
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

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;

        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
            }

            Destroy(gameObject); // Détruire l'ennemi après la collision avec le joueur
        }
    }
}
