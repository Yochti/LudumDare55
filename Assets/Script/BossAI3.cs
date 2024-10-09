using UnityEngine;

public class BossAI3 : MonoBehaviour
{
    public Transform[] firePoints;
    public GameObject bulletPrefab;
    public float moveSpeed = 6f;
    public float rotationSpeed = 90f;
    public float fireRate = 0.2f;
    public float bulletSpeed = 10f;

    private Transform player;
    private float nextFireTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        MoveTowardsPlayer();
        Fire();
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void Fire()
    {
        if (Time.time >= nextFireTime)
        {
            foreach (Transform firePoint in firePoints)
            {
                // Création de la balle avec la rotation du firePoint
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                // Définition de la vitesse de la balle
                bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed;
            }
            nextFireTime = Time.time + fireRate;
        }
    }
}
