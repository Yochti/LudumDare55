using UnityEngine;

public class PortalDivision : MonoBehaviour
{
    public GameObject dividedBulletPrefab; 
    public GameObject dividedBulletPrefabG; 
    public GameObject dividedBulletPrefabS; 
    public int numberOfBullet = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            DivideBullet(collision.gameObject);
        }
        if (collision.CompareTag("BulletG"))
        {
            DivideBullet2(collision.gameObject);
        }if (collision.CompareTag("BulletS"))
        {
            DivideBullet3(collision.gameObject);
        }
    }

    private void DivideBullet(GameObject bullet)
    {
        for (int i = 0; i < numberOfBullet; i++)
        {
            GameObject dividedBullet = Instantiate(dividedBulletPrefab, bullet.transform.position, Quaternion.identity);

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            dividedBullet.GetComponent<Rigidbody2D>().velocity = bullet.GetComponent<Rigidbody2D>().velocity + randomDirection * 2f;

            Destroy(bullet);
        }
    }
    private void DivideBullet2(GameObject bullet)
    {
        for (int i = 0; i < numberOfBullet; i++)
        {
            GameObject dividedBullet = Instantiate(dividedBulletPrefabG, bullet.transform.position, Quaternion.identity);

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            dividedBullet.GetComponent<Rigidbody2D>().velocity = bullet.GetComponent<Rigidbody2D>().velocity + randomDirection * 2f;

            Destroy(bullet);
        }
    }
    private void DivideBullet3(GameObject bullet)
    {
        for (int i = 0; i < numberOfBullet; i++)
        {
            GameObject dividedBullet = Instantiate(dividedBulletPrefabS, bullet.transform.position, Quaternion.identity);

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            dividedBullet.GetComponent<Rigidbody2D>().velocity = bullet.GetComponent<Rigidbody2D>().velocity + randomDirection * 2f;

            Destroy(bullet);
        }
    }
}