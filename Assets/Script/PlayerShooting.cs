using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public AudioSource shootSFX;
    public float bulletForce = 5f;
    public float attackSpeed = .45f;
    public float attackSpeedChnage;
    public saveSytem save;
    private float timeSinceLastShot = 0f;
    private void Start()
    {
        if(save.PlayerAttackSpeed !=0) 
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
}
