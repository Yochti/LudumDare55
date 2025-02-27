using System.Collections;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform playerTransform;
    public static int numberOfProjectiles = 4;
    public float spawnRadius = 2f;
    public float fireRate = 1f;
    public float arcHeightY = 3f;  // Contrôle la hauteur de la parabole (axe Y)
    public float projectileSpeed = 1.5f;  // Vitesse du projectile
    public float descentDepth = 2f; // Profondeur finale (descente)
    public float spawnSpacing = 1f; // Espace entre chaque projectile lors du spawn
    public float delayBetweenProjectiles = 0.1f; // Délai entre chaque projectile

    private bool canShoot = true;

    private void Start()
    {
        numberOfProjectiles = 0;
    }
    void Update()
    {
        if (canShoot)
        {
            StartCoroutine(SpawnProjectiles());
        }
    }

    IEnumerator SpawnProjectiles()
    {
        canShoot = false;

        float angleStep = 360f / numberOfProjectiles; // Calcul de l'angle entre chaque projectile

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = i * angleStep;
            Vector2 spawnPosition = playerTransform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * spawnRadius;

            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
            StartCoroutine(MoveProjectile(projectile));

            yield return new WaitForSeconds(delayBetweenProjectiles);  // Délai entre chaque spawn de projectile
        }

        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    IEnumerator MoveProjectile(GameObject projectile)
    {
        Vector2 startPosition = projectile.transform.position;
        Vector2 endPosition = startPosition + new Vector2(0f, -descentDepth);  // Descente du projectile sur l'axe Y

        float duration = Vector2.Distance(startPosition, endPosition) / projectileSpeed;
        float elapsed = 0f;

        while (elapsed <= duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float height = Mathf.Sin(Mathf.PI * t) * arcHeightY;  // Hauteur contrôlée par arcHeightY
            projectile.transform.position = Vector2.Lerp(startPosition, endPosition, t) + Vector2.up * height;
            yield return null;
        }

        Destroy(projectile);  // Détruit le projectile à la fin de son mouvement
    }
}
