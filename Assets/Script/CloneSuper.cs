using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CloneSuper : MonoBehaviour
{
    public GameObject clone;
    public Image cooldownImage;
    public Sprite cloneSprite;
    public float baseCooldown = 45f;    // Cooldown de 45 secondes
    public float cloneActiveTime = 15f; // Durée pendant laquelle le clone reste actif
    public float countdown;             // Cooldown actuel
    private bool isCloneActive = false;  // État du clone (actif ou non)

    private void Start()
    {
        cooldownImage.sprite = cloneSprite; // Assigner le sprite à l'image de cooldown
        countdown = 45f;                     // Initialiser le countdown à 0
        cooldownImage.fillAmount = 1f;     // Commencer l'image pleine
    }

    private void Update()
    {
        baseCooldown = 40f - (1 * PlayerStats.superCooldownR);
        GameObject player = transform.parent.parent.gameObject; // Obtenir le joueur

        clone.transform.rotation = player.transform.rotation;

        if (countdown < 45f)
        {
            countdown += Time.deltaTime;
            cooldownImage.fillAmount = countdown / baseCooldown; // Met à jour le remplissage de l'image
        }

        if (Input.GetMouseButtonDown(1) && countdown >= 45f && !isCloneActive)
        {
            ActivateClone();
        }
    }

    private void ActivateClone()
    {
        GameObject player = transform.parent.parent.gameObject; // Obtenir le joueur
        clone.transform.position = player.transform.position + player.transform.right; // Position à droite du joueur
        clone.transform.rotation = player.transform.rotation;
        clone.SetActive(true);
        isCloneActive = true;
        countdown = 0f;
        cooldownImage.fillAmount = 0f; // L'image commence à 0 lorsque le clone est activé
        StartCoroutine(DeactivateCloneAfterTime());
    }

    private IEnumerator DeactivateCloneAfterTime()
    {
        yield return new WaitForSeconds(cloneActiveTime);
        clone.SetActive(false);
        isCloneActive = false;
    }
}
