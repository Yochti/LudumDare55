using UnityEngine;

public class Ally1Controller : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform[] firePoints;
    public float shootingRange = 10f;
    public float shootingCooldown = 0.5f;
    public float maxDistanceFromPlayer = 5f;
    public float bulletSpeed = 20f;
    public float moveSpeed = 8f; 

    private float shootingTimer = 0f;

    void Update()
    {

        if (Vector2.Distance(transform.position, player.position) <= shootingRange)
        {

            Vector2 direction = player.position - transform.position;
            transform.up = direction.normalized;


            if (shootingTimer <= 0f)
            {
                Shoot();
                shootingTimer = shootingCooldown;
            }
            else
            {
                shootingTimer -= Time.deltaTime;
            }
        }


        if (Vector2.Distance(transform.position, player.position) > maxDistanceFromPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void Shoot()
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

        if (nearestEnemy != null)
        {
            foreach (Transform firePoint in firePoints)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

                Vector2 directionToEnemy = (nearestEnemy.transform.position - firePoint.position).normalized;

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = directionToEnemy * bulletSpeed;
            }
        }
    }
}
