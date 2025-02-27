using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform playerTransform;  // R�f�rence au Transform du joueur pour modifier sa rotation
    public AudioSource shootSFX;
    public float bulletForce = 5f;
    public float attackSpeed = .45f;
    public float attackSpeedChange;
    public saveSytem save;

    public bool autoShoot = false;     // Bool�en pour tir automatique
    public bool autoAim = false;       // Bool�en pour vis�e automatique vers l'ennemi le plus proche
    public bool findBoss = true;      // Bool�en pour prioriser la vis�e du boss
    public float detectionRadius = 10f; // Rayon maximal de d�tection
    public float baseAttackSpeed;
    private float timeSinceLastShot = 0f;

    private void Start()
    {
        StartCoroutine(waitingForAwake());
    }

    void Update()
    {
        attackSpeed = baseAttackSpeed -  (PlayerStats.attackSpeed * attackSpeed * 0.5f);
        autoShoot = save.isAutoShoot;
        autoAim = save.isAutoAim;
        float realAttackSpeed = attackSpeed - attackSpeedChange;
        timeSinceLastShot += Time.deltaTime;

        Transform target = autoAim ? FindTarget() : null;  // Obtenir la cible pour l'auto-aim si activ�

        if (autoAim && target != null)
            AimAtTarget(target);  // Vise automatiquement la cible en fonction des param�tres

        // V�rifie si autoShoot et autoAim sont activ�s et qu'une cible est pr�sente avant de tirer
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
                return boss.transform;  // Si `findBoss` est activ� et qu'un boss est dans le rayon, vise le boss
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Trouve l'ennemi le plus proche dans le rayon de d�tection
        var closestEnemy = enemies
            .Where(enemy => Vector2.Distance(playerTransform.position, enemy.transform.position) <= detectionRadius)
            .OrderBy(enemy => Vector2.Distance(playerTransform.position, enemy.transform.position))
            .FirstOrDefault();

        return closestEnemy != null ? closestEnemy.transform : null;
    }

    IEnumerator waitingForAwake()
    {
        yield return new WaitForSeconds(0.0001f);
    }
}
