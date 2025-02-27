using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class playerShootingGun3 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform firePoint2;
    public Transform playerTransform; // Référence au Transform du joueur pour l'auto-aim
    public AudioSource shootSFX;
    public float bulletForce = 5f;
    public float attackSpeed = .45f;
    public float attackSpeed2 = .45f;
    public float attackSpeedChnage;
    public float attackSpeedChnage2;
    public saveSytem save;
    public GameObject Super;

    public bool autoShoot = false; // Booléen pour activer le tir automatique
    public bool autoAim = false;   // Booléen pour activer l'auto-aim
    public bool findBoss = false;  // Booléen pour prioriser la visée du boss
    public float detectionRadius = 10f; // Rayon de détection des ennemis
    private float baseAttackSpeed;
    private float timeSinceLastShot = 0f;

    private void Start()
    {

        baseAttackSpeed = PlayerStats.attackSpeed;


        Super.SetActive(false);
    }

    void Update()
    {
        
        attackSpeed = .45f - (PlayerStats.attackSpeed * attackSpeed * 0.5f);
        attackSpeed2 = .45f - (PlayerStats.attackSpeed * attackSpeed2 * 0.5f);
        
        autoShoot = save.isAutoShoot;
        autoAim = save.isAutoAim;
        float realAttackSpeed = attackSpeed - attackSpeedChnage;
        float realAttackSpeed2 = attackSpeed2 - attackSpeedChnage2;

        timeSinceLastShot += Time.deltaTime;

        // Recherche de la cible la plus proche si l'auto-aim est activé
        Transform target = autoAim ? FindTarget() : null;

        // Si l'auto-aim est activé et une cible est trouvée, ajuste la rotation du joueur vers la cible
        if (autoAim && target != null)
            AimAtTarget(target);

        // Tir automatique si une cible est détectée et les deux booléens sont activés
        if (autoShoot && (target != null || !autoAim) && timeSinceLastShot >= realAttackSpeed)
        {
            Shoot();
            Shoot1();
            timeSinceLastShot = 0f;
        }
        else if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1) && timeSinceLastShot >= realAttackSpeed)
        {
            Shoot();
            Shoot1();
            timeSinceLastShot = 0f;
        }
        else if (Input.GetKey(KeyCode.Mouse1) && timeSinceLastShot >= realAttackSpeed2)
        {
            Shoot1();
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        shootSFX.Play();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    void Shoot1()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        shootSFX.Play();
        rb.AddForce(firePoint2.up * bulletForce, ForceMode2D.Impulse);
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
