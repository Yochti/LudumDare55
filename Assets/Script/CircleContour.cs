using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleContour : MonoBehaviour
{
    public int segments = 100; // Nombre de segments pour rendre le cercle lisse
    public float radius = 1f; // Rayon du cercle
    public Color lineColor = Color.white; // Couleur des contours

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.useWorldSpace = false; 
        lineRenderer.positionCount = segments + 1; 
        lineRenderer.startWidth = 0.1f; 
        lineRenderer.endWidth = 0.1f; 
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor; 

        DrawCircle();
    }

    void DrawCircle()
    {
        float angle = 0f;

        for (int i = 0; i < segments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));

            angle += 360f / segments;
        }
    }
}
