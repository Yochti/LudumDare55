using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShootingGun3 : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform firePoint2;
    public AudioSource shootSFX;
    public float bulletForce = 5f;
    public float attackSpeed = .45f;
    public float attackSpeed2 = .45f;
    public float attackSpeedChnage;
    public float attackSpeedChnage2;
    public saveSytem save;
    private float timeSinceLastShot = 0f;
    public PortalManager portal;
    private void Start()
    {
        if (save.PlayerAttackSpeed != 0)
            attackSpeed = save.PlayerAttackSpeed;
        portal.enabled = false;

    }
    void Update()
    {
        float realAttackSpeed = attackSpeed - attackSpeedChnage;
        float realAttackSpeed2 = attackSpeed2 - attackSpeedChnage2;

        timeSinceLastShot += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1))
        {
            if (timeSinceLastShot >= realAttackSpeed)
            {
                Shoot();
                Shoot1();
                timeSinceLastShot = 0f;
            }
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (timeSinceLastShot >= realAttackSpeed2)
            {
                Shoot1();
                timeSinceLastShot = 0f;
            }
        }
        if(Input.GetKey(KeyCode.Mouse0))
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
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
