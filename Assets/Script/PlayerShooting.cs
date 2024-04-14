using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public AudioSource shootSFX;
 

    public float bulletForce = 20f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Shoot(); 
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
