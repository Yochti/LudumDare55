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
    public PlayerMovement pM;
    public float attractionSpeed = 10f; // Vitesse d'attraction de l'orbe
    public float snapDistance = 0.5f; // Distance minimale pour un déplacement instantané
    public saveSytem save;
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
        if (soul.CompareTag("Soul") )
        {
            if (!save.xpAmountDifficulty) xpValue = 10;
            else xpValue = 6;

        }
        else if (soul.CompareTag("Soul2"))
        {
            if (!save.xpAmountDifficulty) xpValue = 150;
            else xpValue = 90;
        }
            
        else if (soul.CompareTag("Soul3"))
        {
            if (!save.xpAmountDifficulty) xpValue = 1000;
            else xpValue = 600;
        }
            

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
