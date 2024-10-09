using UnityEngine;

public class PushTower : MonoBehaviour
{
    public float pushForce = 10f; // La force avec laquelle la tourelle pousse les ennemis
    public float radius = 5f; // La portée de la tourelle

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = other.transform.position - transform.position;
                direction.y = 0f; // Ne pas pousser l'ennemi vers le haut

                rb.AddForce(direction.normalized * pushForce, ForceMode2D.Impulse);
            }
        }
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
