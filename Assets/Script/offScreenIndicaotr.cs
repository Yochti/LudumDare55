using UnityEngine;
using UnityEngine.UI;

public class OffScreenIndicatoor : MonoBehaviour
{
    public Transform player; // Référence au transform du joueur
    public Image indicatorImages; // Image de l'indicateur hors écran
    public float minDistance = 30f; // Distance minimale à laquelle un ennemi doit être pour afficher l'indicateur
    private RectTransform canvasRectTransform;

    void Start()
    {
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    void Update()
    {
        // Trouver tous les ennemis dans la scène
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Trouver l'ennemi le plus proche du joueur
        Transform closestEnemy = GetClosestEnemy(enemies);

        if (closestEnemy != null)
        {
            // Calculer la distance entre le joueur et l'ennemi le plus proche
            float distance = Vector3.Distance(player.position, closestEnemy.position);

            // Vérifier si la distance est supérieure ou égale à la distance minimale pour afficher l'indicateur
            if (distance >= minDistance)
            {
                // Calculer la direction de l'ennemi par rapport au joueur
                Vector3 direction = closestEnemy.position - player.position;
                direction.z = 0f;
                direction.Normalize();

                // Calculer la position de l'indicateur
                Vector3 indicatorPosition = player.position + direction * minDistance;

                // Convertir la position de l'indicateur en position écran
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(indicatorPosition);

                // Limiter la position de l'indicateur aux dimensions du canvas
                Vector2 clampedScreenPosition = new Vector2(
                    Mathf.Clamp(screenPosition.x, 0f, canvasRectTransform.rect.width),
                    Mathf.Clamp(screenPosition.y, 0f, canvasRectTransform.rect.height)
                );

                // Limiter la position de l'indicateur aux limites de l'écran
                clampedScreenPosition.x = Mathf.Clamp(clampedScreenPosition.x, 0f, Screen.width);
                clampedScreenPosition.y = Mathf.Clamp(clampedScreenPosition.y, 0f, Screen.height);

                // Mettre à jour la position de l'indicateur sur le canvas
                indicatorImages.rectTransform.position = clampedScreenPosition;

                // Activer l'indicateur
                indicatorImages.enabled = true;
            }
            else
            {
                // Désactiver l'indicateur si la distance est inférieure à la distance minimale
                indicatorImages.enabled = false;
            }
        }
        else
        {
            // Désactiver l'indicateur si aucun ennemi n'est détecté
            indicatorImages.enabled = false;
        }
    }

    // Méthode pour trouver l'ennemi le plus proche
    Transform GetClosestEnemy(GameObject[] enemies)
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(player.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy.transform;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }
}
