using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundShapesController : MonoBehaviour
{
    public List<RectTransform> shapes; // Assigner manuellement les formes dans l'Inspector
    public float minSpeed = 5f;  // Vitesse minimale du mouvement
    public float maxSpeed = 20f; // Vitesse maximale du mouvement
    public float minRotationSpeed = 5f;  // Vitesse de rotation minimale
    public float maxRotationSpeed = 30f; // Vitesse de rotation maximale
    public Vector2 movementBounds = new Vector2(1920, 1080); // Limites de l'écran

    private Vector2[] directions;
    private float[] speeds;
    private float[] rotationSpeeds;

    void Start()
    {
        int shapeCount = shapes.Count;
        directions = new Vector2[shapeCount];
        speeds = new float[shapeCount];
        rotationSpeeds = new float[shapeCount];

        for (int i = 0; i < shapeCount; i++)
        {
            // Direction aléatoire
            directions[i] = Random.insideUnitCircle.normalized;

            // Vitesse de mouvement aléatoire
            speeds[i] = Random.Range(minSpeed, maxSpeed);

            // Vitesse de rotation aléatoire
            rotationSpeeds[i] = Random.Range(minRotationSpeed, maxRotationSpeed) * (Random.value > 0.5f ? 1 : -1);
        }
    }

    void Update()
    {
        for (int i = 0; i < shapes.Count; i++)
        {
            MoveShape(shapes[i], i);
        }
    }

    void MoveShape(RectTransform shape, int index)
    {
        // Utilise unscaledDeltaTime pour ignorer le timeScale à 0
        float delta = Time.unscaledDeltaTime;

        shape.anchoredPosition += directions[index] * speeds[index] * delta;

        // Optionnel : rotation
        shape.Rotate(Vector3.forward, rotationSpeeds[index] * delta);

        // Si la forme sort de l'écran, elle revient de l'autre côté
        if (shape.anchoredPosition.x > movementBounds.x / 2)
            shape.anchoredPosition = new Vector2(-movementBounds.x / 2, shape.anchoredPosition.y);
        if (shape.anchoredPosition.x < -movementBounds.x / 2)
            shape.anchoredPosition = new Vector2(movementBounds.x / 2, shape.anchoredPosition.y);
        if (shape.anchoredPosition.y > movementBounds.y / 2)
            shape.anchoredPosition = new Vector2(shape.anchoredPosition.x, -movementBounds.y / 2);
        if (shape.anchoredPosition.y < -movementBounds.y / 2)
            shape.anchoredPosition = new Vector2(shape.anchoredPosition.x, movementBounds.y / 2);
    }
}
