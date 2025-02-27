using UnityEngine;

public class projectileDamage : MonoBehaviour
{
    public int damageAmount = 20;
    public static float lifetime = 1.2f;
    public float rotationSpeed = 360f;  // Vitesse de rotation en degr�s par seconde

    private void Start()
    {
        //Invoke("destroyGO", lifetime);
    }

    private void Update()
    {
        // Rotation du projectile autour de l'axe Z
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // R�cup�re le script de l'ennemi ou du boss qui g�re les points de vie
            EnemmiHealth enemyHealth = collision.GetComponent<EnemmiHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }

        }
        if (collision.CompareTag("Boss"))
        {
            Boss1Health enemyHealth = collision.GetComponent<Boss1Health>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }

        }
    }

    /*void destroyGO()
    {
        Destroy(this.gameObject);
    }*/
}
