using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LinkerManager : MonoBehaviour
{
    public GameObject linkerPrefab;
    public bool hasThirdNode = false;

    private Transform[] linkerParts = new Transform[3];
    private LineRenderer lineRenderer;

    private bool slowZoneCreated = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Spawn des deux premiers points
        linkerParts[0] = Instantiate(linkerPrefab, transform.position + Vector3.right * 2f, Quaternion.identity, transform).transform;
        linkerParts[1] = Instantiate(linkerPrefab, transform.position + Vector3.left * 2f, Quaternion.identity, transform).transform;

        if (hasThirdNode)
            ActivateThirdLinker();
    }

    void Update()
    {
        UpdateLineRenderer();

        // Met à jour la zone de slow si les 3 points sont actifs
        if (hasThirdNode && linkerParts[0] && linkerParts[1] && linkerParts[2] && !slowZoneCreated)
        {
            CreateSlowZone();
        }
    }

    public void ActivateThirdLinker()
    {
        if (linkerParts[2] != null) return;

        linkerParts[2] = Instantiate(linkerPrefab, transform.position + Vector3.up * 2f, Quaternion.identity, transform).transform;
        hasThirdNode = true;

        GameObject slowZone = new GameObject("LinkerSlowZone");
        slowZone.transform.position = transform.position;
        var zone = slowZone.AddComponent<LinkerSlowZone>();
        zone.Initialize(linkerParts); 
    }


    void CreateSlowZone()
    {
        Vector3 center = (linkerParts[0].position + linkerParts[1].position + linkerParts[2].position) / 3f;
        GameObject slowZone = new GameObject("LinkerSlowZone");
        slowZone.transform.position = center;
        slowZone.transform.SetParent(transform); // reste attaché au manager
        slowZone.AddComponent<LinkerSlowZone>(); // ajoute le script de ralentissement
        slowZoneCreated = true;
    }

    void UpdateLineRenderer()
    {
        if (linkerParts[0] == null || linkerParts[1] == null)
            return;

        if (!hasThirdNode || linkerParts[2] == null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, linkerParts[0].position);
            lineRenderer.SetPosition(1, linkerParts[1].position);
        }
        else
        {
            lineRenderer.positionCount = 4; // triangle fermé
            lineRenderer.SetPosition(0, linkerParts[0].position);
            lineRenderer.SetPosition(1, linkerParts[1].position);
            lineRenderer.SetPosition(2, linkerParts[2].position);
            lineRenderer.SetPosition(3, linkerParts[0].position); // boucle
        }
    }

    // Optionnel : expose les positions si un autre script en a besoin
    public Transform[] GetLinkerParts() => linkerParts;
}
