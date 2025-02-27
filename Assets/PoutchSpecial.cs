using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PoutchSpecial : MonoBehaviour
{
    public float baseCooldown = 18f;  // Durée de base du cooldown
    public GameObject poutch;
    public Transform player;
    public Image cooldownImage;       // Image de l'interface pour afficher le cooldown
    public Sprite poutchSprite;       // Sprite affiché pendant le cooldown
    private float countdown;          // Timer actuel du cooldown

    private void Start()
    {
        countdown = baseCooldown;          // Initialise le cooldown à la valeur de base
        cooldownImage.sprite = poutchSprite; // Associe le sprite au cooldown
        cooldownImage.fillAmount = 1f;     // L'image est pleine au début
    }

    private void Update()
    {
        baseCooldown = 18f * (1 - PlayerStats.superCooldownR);
        if (countdown < baseCooldown)
        {
            countdown += Time.deltaTime;
            cooldownImage.fillAmount = countdown / baseCooldown;
        }

        if (Input.GetMouseButtonDown(1) && countdown >= baseCooldown)
        {
            ActivatePoutch();
        }
    }

    private void ActivatePoutch()
    {
        Vector3 spawnPosition = player.position + player.up * 1f;
        Instantiate(poutch, spawnPosition, Quaternion.identity);

        countdown = 0f;
        cooldownImage.fillAmount = 0f;

    }


}
