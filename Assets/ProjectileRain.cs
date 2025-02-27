using System.Collections;
using UnityEngine;

public class ProjectileRain : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab; // Le prefab du projectile
    public float projectileSpeed = 10f; // Vitesse des projectiles

    [Header("Spawn Settings")]
    public static int numberOfProjectiles = 0; 
    public float spawnInterval = 0.5f; // Intervalle entre chaque projectile
    public float spawnAreaWidth = 10f; // Largeur de la zone de spawn

    [Header("Cooldown Settings")]
    public float cooldown = 5f; // Temps avant de pouvoir réactiver le pouvoir
    private bool isCooldown = false; // Pour vérifier si le pouvoir est en cooldown

    private Transform player; // Référence au joueur

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(ProjectileRainRoutine());
    }

    private IEnumerator ProjectileRainRoutine()
    {
        while (true) // Boucle infinie pour permettre le réapparition
        {
            if (!isCooldown)
            {
                yield return SpawnProjectiles();
                yield return new WaitForSeconds(cooldown); // Attendre la fin du cooldown
            }
            else
            {
                yield return null;
            }
        }
    }

    private IEnumerator SpawnProjectiles()
    {
        isCooldown = true;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            SpawnSingleProjectile();
            yield return new WaitForSeconds(spawnInterval);
        }

        isCooldown = false;
    }

    private void SpawnSingleProjectile()
    {
        // Calculer une position aléatoire autour du joueur
        float randomX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
        Vector3 spawnPosition = new Vector3(player.position.x + randomX, player.position.y + 10f, 0); // Au-dessus du joueur

        // Instancier le projectile
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        // Appliquer un mouvement vers le bas
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * projectileSpeed;
        }

        // Attacher un script pour gérer les collisions
        BulletPlayerLifetime behaviour = projectile.AddComponent<BulletPlayerLifetime>();

        // Détruire le projectile après 5 secondes (ou ajustez selon vos besoins)
        Destroy(projectile, 5f);
    }
}
