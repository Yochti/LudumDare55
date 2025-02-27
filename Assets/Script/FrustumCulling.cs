using System.Collections;
using UnityEngine;

public class FrustumCulling : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject[] objectsToCull; // Liste des objets � culler (d�sactiver si en dehors de la vue)
    public float checkInterval = 0.1f; // Intervalle de temps entre les v�rifications

    private Plane[] frustumPlanes;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Lancer la coroutine pour v�rifier les objets � intervalle r�gulier
        StartCoroutine(CheckObjectsVisibility());
    }

    // Coroutine pour v�rifier la visibilit� des objets
    private IEnumerator CheckObjectsVisibility()
    {
        while (true)
        {
            // Calculer les plans du frustum de la cam�ra
            frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

            foreach (GameObject obj in objectsToCull)
            {
                if (obj != null)
                {
                    Renderer objRenderer = obj.GetComponent<Renderer>();

                    if (objRenderer != null)
                    {
                        // V�rifier si l'objet est visible ou non
                        if (GeometryUtility.TestPlanesAABB(frustumPlanes, objRenderer.bounds))
                        {
                            obj.SetActive(true); // Activer l'objet s'il est dans le champ de vision
                        }
                        else
                        {
                            obj.SetActive(false); // D�sactiver s'il est hors du champ de vision
                        }
                    }
                }
            }

            // Attendre l'intervalle sp�cifi� avant de refaire une v�rification
            yield return new WaitForSeconds(checkInterval);
        }
    }
}
