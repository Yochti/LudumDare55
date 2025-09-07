using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerShootingAk : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform playerTransform; // R�f�rence pour le Transform du joueur pour l'auto-aim
    public AudioSource shootSFX;
    public float bulletForce = 5f;
    public float attackSpeed = .85f;
    public saveSytem save;
    public int numberBullet = 5;
    public float timeSinceLastShot = 0f;
    public bool autoShoot = false; // Bool�en pour activer le tir automatique
    public bool autoAim = false;   // Bool�en pour activer l'auto-aim
    public bool findBoss = false;  // Bool�en pour prioriser la vis�e du boss
    public float detectionRadius = 10f; // Rayon de d�tection des ennemis

    private void Start()
    {



    }

    void Update()
    {
        attackSpeed = .85f  - (PlayerStats.attackSpeed * attackSpeed);
        autoShoot = save.isAutoShoot;
        autoAim = save.isAutoAim;
        timeSinceLastShot += Time.deltaTime;

        // Recherche de la cible la plus proche si l'auto-aim est activ�
        Transform target = autoAim ? FindTarget() : null;

        // Si l'auto-aim est activ� et une cible est trouv�e, ajuste la rotation du joueur vers la cible
        if (autoAim && target != null)
            AimAtTarget(target);

        // Tir automatique si une cible est d�tect�e et les deux bool�ens sont activ�s
        if (autoShoot && (target != null || !autoAim) && timeSinceLastShot >= attackSpeed)
        {
            StartCoroutine(ShootBurst());
            timeSinceLastShot = 0f;
        }
        else if (Input.GetKey(KeyCode.Mouse0) && timeSinceLastShot >= attackSpeed)
        {
            StartCoroutine(ShootBurst());
            timeSinceLastShot = 0f;
        }
    }

    IEnumerator ShootBurst()
    {
        for (int i = 0; i < numberBullet; i++)
        {
            Shoot();  // Tir d'une seule balle
            yield return new WaitForSeconds(0.1f);  
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        shootSFX.Play();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    private void AimAtTarget(Transform target)
    {
        Vector2 direction = (target.position - playerTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        playerTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));  // Ajuste la rotation du joueur vers la cible
    }

    private Transform FindTarget()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if (findBoss && boss != null)
        {
            if (Vector2.Distance(playerTransform.position, boss.transform.position) <= detectionRadius)
                return boss.transform; // Priorise le boss si trouv� dans le rayon
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var closestEnemy = enemies
            .Where(enemy => Vector2.Distance(playerTransform.position, enemy.transform.position) <= detectionRadius)
            .OrderBy(enemy => Vector2.Distance(playerTransform.position, enemy.transform.position))
            .FirstOrDefault();

        return closestEnemy != null ? closestEnemy.transform : null;
    }
}
