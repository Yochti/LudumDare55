using UnityEngine;

public class EnemyCulling : MonoBehaviour
{
    public Camera mainCamera; // R�f�rence � la cam�ra principale
    public LayerMask enemyLayer; // Couche des ennemis
    private Plane[] frustumPlanes; // Les plans du frustum de la cam�ra

    private Renderer[] enemyRenderers; // Les composants de rendu de l'ennemi
    private bool isVisible = true; // Indicateur pour savoir si l'ennemi est visible

    private void Start()
    {
        // Si la cam�ra principale n'est pas assign�e, on prend la cam�ra par d�faut
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Obtenir tous les Renderer (sprites, meshes) de cet ennemi
        enemyRenderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        // Obtenir les plans du frustum de la cam�ra
        frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        // V�rifier si l'ennemi est dans le champ de vision de la cam�ra
        if (IsInView())
        {
            if (!isVisible)
            {
                // Si l'ennemi devient visible, on r�active le rendu
                SetRenderersActive(true);
                isVisible = true;
            }
        }
        else
        {
            if (isVisible)
            {
                // Si l'ennemi n'est plus visible, on d�sactive le rendu
                SetRenderersActive(false);
                isVisible = false;
            }
        }
    }

    // M�thode pour v�rifier si l'ennemi est dans le champ de vision de la cam�ra
    private bool IsInView()
    {
        // Teste si le bounding box de l'ennemi est dans le frustum de la cam�ra
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
