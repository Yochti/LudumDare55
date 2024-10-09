using UnityEngine;
using System.Collections;

public class PlayerShootingAk : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public AudioSource shootSFX;
    public float bulletForce = 5f;
    public float attackSpeed = .85f;
    public float attackSpeedChnage;
    public saveSytem save;
    public int numberBullet = 5;
    public float timeSinceLastShot = 0f;

    private void Start()
    {
        if (save.PlayerAttackSpeed != 0)
            attackSpeed = save.PlayerAttackSpeed;
    }

    void Update()
    {
        float realAttackSpeed = attackSpeed - attackSpeedChnage;

        timeSinceLastShot += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (timeSinceLastShot >= realAttackSpeed)
            {
                StartCoroutine(ShootBurst());  // Appelle la rafale via coroutine
                timeSinceLastShot = 0f;
            }
        }
    }

    IEnumerator ShootBurst()
    {
        for (int i = 0; i < numberBullet; i++)
        {
            Shoot();  // Tir d'une seule balle
            yield return new WaitForSeconds(0.07f);  // Intervalle entre chaque balle de la rafale
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        shootSFX.Play();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
