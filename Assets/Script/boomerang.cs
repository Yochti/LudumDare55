using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float initialSpeed = 10f;  // Vitesse initiale du boomerang
    public float returnSpeed = 15f;   // Vitesse de retour du boomerang
    public float flightDuration = 1.5f;  // Temps avant que le boomerang ne commence à ralentir
    public int damageAmount = 10;     // Dégâts infligés par le boomerang

    private Vector2 direction;
    private bool isReturning = false;
    private float flightTime = 0f;
    private Transform player;

    public void Initialize(Vector2 dir, Transform playerTransform)
    {
        direction = dir.normalized;
        player = playerTransform;
    }

    void Update()
    {
        flightTime += Time.deltaTime;

        if (!isReturning)
        {
            // Le boomerang se déplace dans la direction initiale
            transform.Translate(direction * initialSpeed * Time.deltaTime);

            if (flightTime >= flightDuration)
            {
                // Commence à ralentir
                isReturning = true;
            }
        }
        else
        {
            // Le boomerang revient vers le joueur
            Vector2 returnDirection = (player.position - transform.position).normalized;
            transform.Translate(returnDirection * returnSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Infliger des dégâts à l'ennemi
            collision.GetComponent<EnemmiHealth>().TakeDamage(damageAmount);
        }
        else if (collision.CompareTag("Player"))
        {
            // Détruire le boomerang s'il touche le joueur
            Destroy(gameObject);
        }
    }
}
