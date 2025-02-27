using UnityEngine;

public class EnemyCulling : MonoBehaviour
{
    public Camera mainCamera; // Référence à la caméra principale
    public LayerMask enemyLayer; // Couche des ennemis
    private Plane[] frustumPlanes; // Les plans du frustum de la caméra

    private Renderer[] enemyRenderers; // Les composants de rendu de l'ennemi
    private bool isVisible = true; // Indicateur pour savoir si l'ennemi est visible

    private void Start()
    {
        // Si la caméra principale n'est pas assignée, on prend la caméra par défaut
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Obtenir tous les Renderer (sprites, meshes) de cet ennemi
        enemyRenderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        // Obtenir les plans du frustum de la caméra
        frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        // Vérifier si l'ennemi est dans le champ de vision de la caméra
        if (IsInView())
        {
            if (!isVisible)
            {
                // Si l'ennemi devient visible, on réactive le rendu
                SetRenderersActive(true);
                isVisible = true;
            }
        }
        else
        {
            if (isVisible)
            {
                // Si l'ennemi n'est plus visible, on désactive le rendu
                SetRenderersActive(false);
                isVisible = false;
            }
        }
    }

    // Méthode pour vérifier si l'ennemi est dans le champ de vision de la caméra
    private bool IsInView()
    {
        // Teste si le bounding box de l'ennemi est dans le frustum de la caméra
        Collider2D enemyCollider = GetComponent<Collider2D>();
        if (enemyCollider != null)
        {
            return GeometryUtility.TestPlanesAABB(frustumPlanes, enemyCollider.bounds);
        }
        return false;
    }

    private void SetRenderersActive(bool isActive)
    {
        foreach (Renderer renderer in enemyRenderers)
        {
            renderer.enabled = isActive;
        }
    }
}
