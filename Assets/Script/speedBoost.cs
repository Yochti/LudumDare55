using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    private float boostDuration = 8; // Durée du boost
    private bool isBoostActive = false; // Indique si le boost est actif
    private float remainingBoostTime; // Temps restant pour le boost
    public SpriteRenderer s1;
    public SpriteRenderer s2;
    public SpriteRenderer s3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isBoostActive)
        {
            // Appliquer le boost
            ActivateBoost();
        }
    }

    private void Update()
    {
        if (isBoostActive)
        {
            // Décompte du timer
            remainingBoostTime -= Time.deltaTime;

            // Fin du boost
            if (remainingBoostTime <= 0)
            {
                DeactivateBoost();
            }
        }
    }

    private void ActivateBoost()
    {
        PlayerStats.moveSpeed += 2.5f;
        PlayerStats.attackSpeed += 0.4f;
        PlayerStats.additionalDamage += 0.2f;

        isBoostActive = true;
        remainingBoostTime = boostDuration;
        s1.enabled = false;
        s2.enabled = false;
        s3.enabled = false;
    }

    private void DeactivateBoost()
    {
        PlayerStats.moveSpeed -= 2.5f;
        PlayerStats.attackSpeed -= 0.4f;
        PlayerStats.additionalDamage -= 0.2f;

        isBoostActive = false;

        // Détruire l'objet
        Destroy(this.gameObject);
    }
}
