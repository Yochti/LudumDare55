using System.Collections;
using UnityEngine;

public class laserAbilitePower : MonoBehaviour
{
    public float speed = 10f;             // Vitesse du boomerang
    public float returnDelay = 2f;        // Temps avant que le boomerang revienne vers le joueur
    public float maxDistance = 5f;        // Distance maximale avant retour
    public int damage = 20;               // Dégâts infligés aux ennemis
    public Vector2 initialDirection = Vector2.down;  // Direction initiale du boomerang (modifiable dans l'inspecteur Unity)

    private Transform player;             // Référence au joueur
    private Rigidbody2D rb;               // Référence au Rigidbody2D du boomerang
    private bool isReturning = false;     // Indique si le boomerang revient
    private Vector2 playerLastPosition;   // Sauvegarde de la dernière position du joueur pour calculer le mouvement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform; // Trouver le joueur avec le tag "Player"

        // Bloquer la rotation du boomerang sur l'axe Z (pour éviter qu'il tourne)
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Calculer la direction initiale du boomerang en fonction de la direction configurée
        playerLastPosition = new Vector2(player.position.x, player.position.y);

        // Lancer le boomerang dans la direction spécifiée
        StartCoroutine(LaunchBoomerang());
    }

    IEnumerator LaunchBoomerang()
    {
        float launchTime = 0f;

        // Le boomerang se déplace dans la direction configurée pendant un certain temps
        while (launchTime < returnDelay && Vector2.Distance(transform.position, player.position) < maxDistance)
        {
            // Utiliser la direction initiale assignée au boomerang, sans prendre en compte le mouvement du joueur
            rb.velocity = initialDirection.normalized * speed;

            // Mettre à jour la dernière position du joueur pour le suivi
            playerLastPosition = new Vector2(player.position.x, player.position.y);

            launchTime += Time.deltaTime;
            yield return null;
        }

        // Le boomerang doit revenir vers le joueur
        StartCoroutine(ReturnToPlayer());
    }

    IEnumerator ReturnToPlayer()
    {
        isReturning = true;

        while (isReturning)
        {
            // Calcul de la direction vers le joueur (indépendant de sa rotation)
            Vector2 directionToPlayer = (new Vector2(player.position.x, player.position.y) - (Vector2)transform.position).normalized;
            rb.velocity = directionToPlayer * speed;

            // Si le boomerang est proche du joueur, arrêter le retour
            if (Vector2.Distance(transform.position, player.position) < 0.5f)
            {
                isReturning = false;
                rb.velocity = Vector2.zero; // Arrête le boomerang une fois qu'il est revenu
                StartCoroutine(SwitchDirection()); // Inverse la direction après le retour
            }
            yield return null;
        }
    }

    IEnumerator SwitchDirection()
    {
        // Inverser la direction du boomerang par rapport à son mouvement initial
        initialDirection = -initialDirection;

        // Recalculer la direction avec le même principe
        yield return LaunchBoomerang();  // Relancer le boomerang dans la direction opposée
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si le boomerang touche un ennemi
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemmiHealth enemy = collision.GetComponent<EnemmiHealth>(); // Assurez-vous que l'ennemi a un script "EnemyHealth"
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Inflige des dégâts à l'ennemi
            }
        }if (collision.gameObject.CompareTag("Boss"))
        {
            Boss1Health enemy = collision.GetComponent<Boss1Health>(); // Assurez-vous que l'ennemi a un script "EnemyHealth"
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Inflige des dégâts à l'ennemi
            }
        }
    }
}
