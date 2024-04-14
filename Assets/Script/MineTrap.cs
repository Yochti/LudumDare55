using UnityEngine;
using System.Collections;

public class MineTrap : MonoBehaviour
{
    public float explosionRadius = 3f;
    public LayerMask enemyLayer;
    public int explosionDamage = 50;
    public AudioSource explosionSFX;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (enemyLayer == (enemyLayer | (1 << other.gameObject.layer)))
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (enemyLayer == (enemyLayer | (1 << collider.gameObject.layer)))
            {
                EnemmiHealth enemyHealth = collider.GetComponent<EnemmiHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(explosionDamage);
                }
                Destroy(collider.gameObject);
            }
        }
        CameraShake.Instance.ShakeCamera(4f, 0.2f);
        StartCoroutine(waitDestroy());
    }

    private IEnumerator waitDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
