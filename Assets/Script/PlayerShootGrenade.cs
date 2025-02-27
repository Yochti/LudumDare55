using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerShootGrenade : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform firePoint;
    public Transform playerTransform;
    public AudioSource shootSFX;
    public float initialBulletForce = 10f;
    public float stopTime = 2f;
    public float attackSpeed = 1f;
    public float attackSpeedChange;
    public saveSytem save;
    private float timeSinceLastShot = 0f;
    public float baseAttackSpeed;
    // Variables pour autoAim et autoShoot
    public bool autoShoot = false;
    public bool autoAim = false;
    public float detectionRadius = 10f;

    private void Start()
    {


    }

    void Update()
    {
        attackSpeed = baseAttackSpeed - (PlayerStats.attackSpeed * attackSpeed * 0.5f);
        autoShoot = save.isAutoShoot;
        autoAim = save.isAutoAim;
        timeSinceLastShot += Time.deltaTime;
        float realAttackSpeed = attackSpeed - attackSpeedChange;

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
        GameObject grenade = Instantiate(grenadePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = grenade.GetComponent<Rigidbody2D>();

        rb.AddForce(firePoint.up * initialBulletForce, ForceMode2D.Impulse);
        shootSFX.Play();

        StartCoroutine(StopGrenade(rb, stopTime));
    }

    private IEnumerator StopGrenade(Rigidbody2D rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    private Transform FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        Transform closestTarget = hits
            .Where(hit => hit.CompareTag("Enemy") || hit.CompareTag("Boss"))
            .OrderBy(hit => Vector2.Distance(transform.position, hit.transform.position))
            .Select(hit => hit.transform)
            .FirstOrDefault();

        return closestTarget;
    }

    // Oriente le joueur vers la cible
    private void AimAtTarget(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        playerTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
