using System.Collections;
using UnityEngine;

public class FrustumCulling : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject[] objectsToCull; // Liste des objets à culler (désactiver si en dehors de la vue)
    public float checkInterval = 0.1f; // Intervalle de temps entre les vérifications

    private Plane[] frustumPlanes;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Lancer la coroutine pour vérifier les objets à intervalle régulier
        StartCoroutine(CheckObjectsVisibility());
    }

    // Coroutine pour vérifier la visibilité des objets
    private IEnumerator CheckObjectsVisibility()
    {
        while (true)
        {
            // Calculer les plans du frustum de la caméra
            frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

            foreach (GameObject obj in objectsToCull)
            {
                if (obj != null)
                {
                    Renderer objRenderer = obj.GetComponent<Renderer>();

                    if (objRenderer != null)
                    {
                        // Vérifier si l'objet est visible ou non
                        if (GeometryUtility.TestPlanesAABB(frustumPlanes, objRenderer.bounds))
                        {
                            obj.SetActive(true); // Activer l'objet s'il est dans le champ de vision
                        }
                        else
                        {
                            obj.SetActive(false); // Désactiver s'il est hors du champ de vision
                        }
                    }
                }
            }

            // Attendre l'intervalle spécifié avant de refaire une vérification
            yield return new WaitForSeconds(checkInterval);
        }
    }
}
