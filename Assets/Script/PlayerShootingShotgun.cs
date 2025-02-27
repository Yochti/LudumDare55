using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
public class PlayerShootingShotgun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform playerTransform;
    public AudioSource shootSFX;
    public float attackSpeed = 0.45f;
    public float attackSpeedChange;
    public int numberOfBullets = 5;
    public float spreadAngle = 15f;
    public float bulletForce = 10f;
    public float bulletSpawnRadius = 0.1f;
    public saveSytem save;
    private float timeSinceLastShot = 0f;

    public bool autoShoot = false;
    public bool autoAim = false;
    public bool findBoss = false;
    public float detectionRadius = 10f;

    private void Start()
    {
        
        
    }

    void Update()
    {
        attackSpeed = .45f - (PlayerStats.attackSpeed * attackSpeed * 0.5f);
        autoShoot = save.isAutoShoot;
        autoAim = save.isAutoAim;
        float realAttackSpeed = attackSpeed - attackSpeedChange;
        timeSinceLastShot += Time.deltaTime;

        Transform target = autoAim ? FindTarget() : null;

        if (autoAim && target != null)
            AimAtTarget(target);

        if (autoShoot && (target != null || !autoAim) && timeSinceLastShot >= realAttackSpeed)
        {
            Shoot();
            timeSinceLastShot = 0f;
        }
        else if (Input.GetKey(KeyCode.Mouse0) && timeSinceLastShot >= realAttackSpeed)
        {
            Shoot();
            timeSinceLastShot = 0f;
        }
    }

    void Shoot()
    {
        float angleStep = spreadAngle / (numberOfBullets - 1);
        float angle = -spreadAngle / 2;

        List<GameObject> spawnedBullets = new List<GameObject>();

        for (int i = 0; i < numberOfBullets; i++)
        {
            Vector2 spawnPosition = firePoint.position + (Vector3)Random.insideUnitCircle * bulletSpawnRadius;

            float bulletDirX = firePoint.up.x * Mathf.Cos(angle * Mathf.Deg2Rad) - firePoint.up.y * Mathf.Sin(angle * Mathf.Deg2Rad);
            float bulletDirY = firePoint.up.x * Mathf.Sin(angle * Mathf.Deg2Rad) + firePoint.up.y * Mathf.Cos(angle * Mathf.Deg2Rad);

            Vector2 bulletDirection = new Vector2(bulletDirX, bulletDirY).normalized;

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = bulletDirection * bulletForce;

            spawnedBullets.Add(bullet);

            angle += angleStep;
        }

        for (int i = 0; i < spawnedBullets.Count; i++)
        {
            Collider2D colliderA = spawnedBullets[i].GetComponent<Collider2D>();

            for (int j = i + 1; j < spawnedBullets.Count; j++)
            {
                Collider2D colliderB = spawnedBullets[j].GetComponent<Collider2D>();
                Physics2D.IgnoreCollision(colliderA, colliderB);
            }
        }

        shootSFX.Play();
    }

    private void AimAtTarget(Transform target)
    {
        Vector2 direction = (target.position - playerTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        playerTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    private Transform FindTarget()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if (findBoss && boss != null)
        {
            if (Vector2.Distance(playerTransform.position, boss.transform.position) <= detectionRadius)
                return boss.transform;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var closestEnemy = enemies
            .Where(enemy => Vector2.Distance(playerTransform.position, enemy.transform.position) <= detectionRadius)
            .OrderBy(enemy => Vector2.Distance(playerTransform.position, enemy.transform.position))
            .FirstOrDefault();

        return closestEnemy != null ? closestEnemy.transform : null;
    }
}
