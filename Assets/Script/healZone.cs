using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : MonoBehaviour
{
    public PlayerHealth pH;
    public static int healAmount = 1;

    private bool isHealing = false; // Bool�en pour v�rifier si la coroutine est en cours

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isHealing) // V�rifie si la coroutine n'est pas d�j� active
        {
            StartCoroutine(HealingCountdown());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isHealing = false; // R�initialise l'�tat de gu�rison lorsque le joueur quitte la zone
        }
    }

    IEnumerator HealingCountdown()
    {
        isHealing = true; // Marque la coroutine comme active

        while (isHealing) // Continue de soigner tant que le joueur est dans la zone
        {
            pH.Heal(healAmount);
            yield return new WaitForSeconds(1f); // Attends 1 seconde avant de soigner � nouveau
        }
    }
}
