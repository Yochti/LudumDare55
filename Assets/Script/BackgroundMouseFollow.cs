using UnityEngine;
using UnityEngine.UI;

public class BackgroundMouseFollow : MonoBehaviour
{
    public RectTransform backgroundImage; // Image de fond � d�placer
    public float movementFactor = 5f; // Facteur de d�placement (petite valeur pour un mouvement subtil)

    private Vector2 initialPosition; // Correction : Vector2 au lieu de Vector3

    private void Start()
    {
        if (backgroundImage == null)
        {
            backgroundImage = GetComponent<RectTransform>();
        }

        initialPosition = backgroundImage.anchoredPosition; // Correct : Vector2
    }

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        // Calcul du d�placement par rapport au centre de l'�cran
        Vector2 offset = (mousePosition - screenCenter) / screenCenter;
        Vector2 movement = offset * movementFactor;

        // Applique le mouvement � l'image de fond (Vector2 + Vector2)
        backgroundImage.anchoredPosition = initialPosition + movement;
    }
}
