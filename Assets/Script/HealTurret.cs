using UnityEngine;

public class HealTurret : MonoBehaviour
{
    public GameObject healPrefab; // Préfabriqué du soin
    public float fireRate = 1f; // Cadence d'émission des soins en secondes
    public float bulletSpeed = 20f; // Vitesse de la balle de soin
    private GameObject player; // Référence vers le joueur
    private float fireCooldown = 0f; // Temps restant avant de pouvoir émettre un soin à nouveau

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (fireCooldown <= 0f)
        {
            ShootHeal();
            fireCooldown = 1f / fireRate;
        }

        fireCooldown -= Time.deltaTime;
    }

    void ShootHeal()
    {
        if (player != null)
        {
            GameObject heal = Instantiate(healPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = heal.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                rb.velocity = direction * bulletSpeed;
            }
        }
    }
}
