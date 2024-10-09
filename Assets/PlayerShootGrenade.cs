using UnityEngine;
using System.Collections;
public class PlayerShootGrenade : MonoBehaviour
{
    public GameObject grenadePrefab; // Le préfabriqué de la grenade
    public Transform firePoint; // Le point de tir de la grenade
    public AudioSource shootSFX; // Le son du tir
    public float initialBulletForce = 10f; // La force initiale appliquée à la grenade
    public float stopTime = 2f; // Temps après lequel la grenade s'arrête
    public float attackSpeed = 1f; // Temps entre chaque tir
    public float attackSpeedChnage;
    public saveSytem save;
    private float timeSinceLastShot = 0f;

    private void Start()
    {
        if (save.PlayerAttackSpeed != 0)
            attackSpeedChnage = save.PlayerAttackSpeed;
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        float realAttackSpeed = attackSpeed - attackSpeedChnage;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (timeSinceLastShot >= attackSpeed)
            {
                Shoot();
                timeSinceLastShot = 0f;
            }
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
        if(rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;

        }
    }
}
