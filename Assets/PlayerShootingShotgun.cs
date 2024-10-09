using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingShotgun : MonoBehaviour
{
    public GameObject bulletPrefab; // Préfabriqué de la balle
    public Transform firePoint; // Point de tir de l'arme
    public AudioSource shootSFX; // Effet sonore du tir
    public float attackSpeed = 0.45f; // Vitesse d'attaque
    public float attackSpeedChange; // Modification de la vitesse d'attaque
    public int numberOfBullets = 5; // Nombre de balles tirées par salve
    public float spreadAngle = 15f; // Angle de dispersion des balles
    public float bulletForce = 10f; // Vitesse des balles
    public float bulletSpawnRadius = 0.1f; // Rayon autour du point de tir pour instancier les balles
    public saveSytem save;
    private float timeSinceLastShot = 0f;

    private void Start()
    {
        if (save.PlayerAttackSpeed != 0)
            attackSpeed = save.PlayerAttackSpeed;
    }

    void Update()
    {
        float realAttackSpeed = attackSpeed - attackSpeedChange;
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (timeSinceLastShot >= realAttackSpeed)
            {
                Shoot();
                timeSinceLastShot = 0f;
            }
        }
    }

    void Shoot()
    {
        float angleStep = spreadAngle / (numberOfBullets - 1); // Espace entre les balles
        float angle = -spreadAngle / 2; // Angle initial pour centrer la dispersion

        List<GameObject> spawnedBullets = new List<GameObject>();

        for (int i = 0; i < numberOfBullets; i++)
        {
            // Position de spawn pour chaque balle
            Vector2 spawnPosition = firePoint.position + (Vector3)Random.insideUnitCircle * bulletSpawnRadius;

            // Calculer la direction de la balle
            float bulletDirX = firePoint.up.x * Mathf.Cos(angle * Mathf.Deg2Rad) - firePoint.up.y * Mathf.Sin(angle * Mathf.Deg2Rad);
            float bulletDirY = firePoint.up.x * Mathf.Sin(angle * Mathf.Deg2Rad) + firePoint.up.y * Mathf.Cos(angle * Mathf.Deg2Rad);

            Vector2 bulletDirection = new Vector2(bulletDirX, bulletDirY).normalized;

            // Instancier la balle
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = bulletDirection * bulletForce;

            // Ajouter la balle à la liste pour ignorer les collisions
            spawnedBullets.Add(bullet);

            angle += angleStep; // Incrémente l'angle pour la balle suivante
        }

        // Ignore les collisions entre les balles instanciées
        for (int i = 0; i < spawnedBullets.Count; i++)
        {
            Collider2D colliderA = spawnedBullets[i].GetComponent<Collider2D>();

            for (int j = i + 1; j < spawnedBullets.Count; j++)
            {
                Collider2D colliderB = spawnedBullets[j].GetComponent<Collider2D>();
                Physics2D.IgnoreCollision(colliderA, colliderB);
            }
        }

        shootSFX.Play(); // Jouer le son de tir
    }
}
