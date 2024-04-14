using UnityEngine;

public class BulletTurret : MonoBehaviour
{
    public GameObject bulletPrefab; // Préfabriqué de la balle
    public float fireRate = 1f; // Cadence de tir en secondes
    public float bulletSpeed = 20f; // Vitesse de la balle
    private float fireCooldown = 0f; // Temps restant avant de pouvoir tirer à nouveau

    void Update()
    {
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }

        fireCooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            Vector2 direction = (nearestEnemy.transform.position - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed;
            }
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < nearestDistance)
            {
                nearestEnemy = enemy;
                nearestDistance = distanceToEnemy;
            }
        }

        return nearestEnemy;
    }
}
