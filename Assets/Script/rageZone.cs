using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrowZone : MonoBehaviour
{
    public static float damageMultiplier = 1.25f;
    private bool isInZone = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isInZone)
        {
            isInZone = true;
            PlayerStats.additionalDamage *= damageMultiplier;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isInZone)
        {
            isInZone = false;
            PlayerStats.additionalDamage /= damageMultiplier;
        }
    }
}
