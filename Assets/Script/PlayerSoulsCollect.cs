using UnityEngine;
using System.Collections;

public class PlayerSoulsCollect : MonoBehaviour
{
    public static int soulValue = 0;
    public AudioSource soulsSFX;
    public HealthBar cointxt;
    public GameObject player; // Assurez-vous que cette référence est assignée
    public PlayerHealth playerH;
    public PlayerXPManager pXP;

    public float attractionSpeed = 10f; // Vitesse d'attraction de l'orbe
    public float snapDistance = 0.5f; // Distance minimale pour un déplacement instantané

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Soul") || other.CompareTag("Soul2") || other.CompareTag("Soul3"))
        {
            StartCoroutine(AttractSoulToPlayer(other.gameObject));
        }
        else if (other.CompareTag("HealPower"))
        {
            playerH.Heal(25);
            Destroy(other.gameObject);
        }
    }

    private IEnumerator AttractSoulToPlayer(GameObject soul)
    {
        // Identifier la valeur de l'XP en fonction du type de l'orbe
        int xpValue = 0;
        if (soul.CompareTag("Soul"))
            xpValue = 10;
        else if (soul.CompareTag("Soul2"))
            xpValue = 150;
        else if (soul.CompareTag("Soul3"))
            xpValue = 1000;

        // Attraction rapide de l'orbe vers le joueur
        while (soul != null)
        {
            float distance = Vector2.Distance(soul.transform.position, player.transform.position);

            // Si l'orbe est suffisamment proche, déplacer directement sur le joueur
            if (distance <= snapDistance)
            {
                soul.transform.position = player.transform.position;
                break;
            }

            // Sinon, déplacement progressif vers le joueur
            Vector3 direction = (player.transform.position - soul.transform.position).normalized;
            soul.transform.position += direction * attractionSpeed * Time.deltaTime;

            yield return null; // Attendre la prochaine frame
        }

        // Si l'orbe existe encore, traiter sa destruction et l'ajout d'XP
        if (soul != null)
        {
            Destroy(soul);
            pXP.AddXP(xpValue);
            soulsSFX.Play();
        }
    }
}
