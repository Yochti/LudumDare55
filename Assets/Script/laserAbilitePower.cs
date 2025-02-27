using System.Collections;
using UnityEngine;

public class laserAbilitePower : MonoBehaviour
{
    public float speed = 10f;             // Vitesse du boomerang
    public float returnDelay = 2f;        // Temps avant que le boomerang revienne vers le joueur
    public float maxDistance = 5f;        // Distance maximale avant retour
    public int damage = 20;               // D�g�ts inflig�s aux ennemis
    public Vector2 initialDirection = Vector2.down;  // Direction initiale du boomerang (modifiable dans l'inspecteur Unity)

    private Transform player;             // R�f�rence au joueur
    private Rigidbody2D rb;               // R�f�rence au Rigidbody2D du boomerang
    private bool isReturning = false;     // Indique si le boomerang revient
    private Vector2 playerLastPosition;   // Sauvegarde de la derni�re position du joueur pour calculer le mouvement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform; // Trouver le joueur avec le tag "Player"

        // Bloquer la rotation du boomerang sur l'axe Z (pour �viter qu'il tourne)
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Calculer la direction initiale du boomerang en fonction de la direction configur�e
        playerLastPosition = new Vector2(player.position.x, player.position.y);

        // Lancer le boomerang dans la direction sp�cifi�e
        StartCoroutine(LaunchBoomerang());
    }

    IEnumerator LaunchBoomerang()
    {
        float launchTime = 0f;

        // Le boomerang se d�place dans la direction configur�e pendant un certain temps
        while (launchTime < returnDelay && Vector2.Distance(transform.position, player.position) < maxDistance)
        {
            // Utiliser la direction initiale assign�e au boomerang, sans prendre en compte le mouvement du joueur
            rb.velocity = initialDirection.normalized * speed;

            // Mettre � jour la derni�re position du joueur pour le suivi
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
            // Calcul de la direction vers le joueur (ind�pendant de sa rotation)
            Vector2 directionToPlayer = (new Vector2(player.position.x, player.position.y) - (Vector2)transform.position).normalized;
            rb.velocity = directionToPlayer * speed;

            // Si le boomerang est proche du joueur, arr�ter le retour
            if (Vector2.Distance(transform.position, player.position) < 0.5f)
            {
                isReturning = false;
                rb.velocity = Vector2.zero; // Arr�te le boomerang une fois qu'il est revenu
                StartCoroutine(SwitchDirection()); // Inverse la direction apr�s le retour
            }
            yield return null;
        }
    }

    IEnumerator SwitchDirection()
    {
        // Inverser la direction du boomerang par rapport � son mouvement initial
        initialDirection = -initialDirection;

        // Recalculer la direction avec le m�me principe
        yield return LaunchBoomerang();  // Relancer le boomerang dans la direction oppos�e
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si le boomerang touche un ennemi
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemmiHealth enemy = collision.GetComponent<EnemmiHealth>(); // Assurez-vous que l'ennemi a un script "EnemyHealth"
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Inflige des d�g�ts � l'ennemi
            }
        }if (collision.gameObject.CompareTag("Boss"))
        {
            Boss1Health enemy = collision.GetComponent<Boss1Health>(); // Assurez-vous que l'ennemi a un script "EnemyHealth"
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Inflige des d�g�ts � l'ennemi
            }
        }
    }
}
