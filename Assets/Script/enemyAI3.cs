using UnityEngine;

public class EnemyAI3 : MonoBehaviour
{
    private Transform player;       // Référence vers le joueur
    private Transform poutch;       // Référence vers le poutch
    public float moveSpeed = 15f;   // Vitesse de déplacement de l'ennemi
    public float explosionRadius = 2f; // Rayon d'explosion de l'ennemi
    public float explosionDamage = 20f; // Dégâts de l'explosion
    public float detectionRadius = 8f; // Rayon autour de la dernière position connue du joueur
    private Vector3 targetPosition; // Position vers laquelle l'ennemi se dirige

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

        // Priorité de déplacement vers le poutch si disponible
        if (poutch != null)
        {
            MoveTowardsTarget(poutch);
            if (Vector2.Distance(transform.position, poutch.position) <= explosionRadius)
            {
                Explode();
            }
        }
        else if (player != null && !PlayerInvisibleZone.isInvisible)
        {
            MoveTowardsTarget(player);
            if (Vector2.Distance(transform.position, player.position) <= explosionRadius)
            {
                Explode();
            }
        }
        else if (player != null)
        {
            // Se déplace vers la dernière position connue si le joueur est invisible
            MoveTowardsApproximatePosition();
            if (Vector2.Distance(transform.position, player.position) <= explosionRadius)
            {
                Explode();
            }
        }
    }

    private void MoveTowardsTarget(Transform target)
    {
        // Déplacement vers la cible (poutch ou joueur)
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    private void MoveTowardsApproximatePosition()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.5f || targetPosition == Vector3.zero)
        {
            targetPosition = GetRandomPointAround(PlayerInvisibleZone.lastKnownPosition);
        }

        // Déplacement vers la position approximative
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    private Vector3 GetRandomPointAround(Vector3 center)
    {
        Vector2 randomPoint = Random.insideUnitCircle * detectionRadius;
        return new Vector3(center.x + randomPoint.x, center.y + randomPoint.y, transform.position.z);
    }

    void Explode()
    {
        // Dégâts de l'explosion sur les objets dans le rayon
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(explosionDamage);
                }
            }
        }

        Destroy(gameObject);
    }
}
