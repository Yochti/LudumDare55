using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public GameObject portalPrefab; // Prefab du portail
    public float shortRange = 5f; // Portée du premier portail
    public float longRange = 20f; // Portée du deuxième portail
    public float portalSpeed = 10f;
    public float portalLifetime = 10f;
    public float portalCountdown = 0f;
    public LayerMask portalLayerMask; // Masque de collision pour les portails

    private GameObject firstPortal; // Premier portail tiré
    private GameObject secondPortal; // Deuxième portail tiré
    private bool isTeleporting = false; // Indique si le joueur est en train de se téléporter
    private bool isFirstPortal = true;
    private Vector3 firstPortalDestination; // Destination du premier portail
    private Vector3 secondPortalDestination; // Destination du deuxième portail

    void Update()
    {
        portalCountdown -= Time.deltaTime;
        if (Input.GetMouseButtonDown(1) && !isTeleporting)
        {
            if (portalCountdown <= 0)
            {
                if (isFirstPortal)
                {
                    Vector3 mousePosition1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    firstPortal = Instantiate(portalPrefab, transform.position, Quaternion.identity);
                    firstPortalDestination = transform.position + (mousePosition1 - transform.position).normalized * shortRange;
                    Vector3 firstPortalVelocity = (firstPortalDestination - firstPortal.transform.position).normalized * portalSpeed;
                    firstPortal.GetComponent<Rigidbody2D>().velocity = firstPortalVelocity;
                    isFirstPortal = false;
                }
                else
                {
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    secondPortal = Instantiate(portalPrefab, transform.position, Quaternion.identity);
                    secondPortalDestination = transform.position + (mousePosition - transform.position).normalized * longRange;
                    Vector3 secondPortalVelocity = (secondPortalDestination - secondPortal.transform.position).normalized * portalSpeed;
                    secondPortal.GetComponent<Rigidbody2D>().velocity = secondPortalVelocity;

                    LateUpdate();
                    isFirstPortal = true;
                    portalCountdown = 10f;
                }
            }
        }

        StopFirstPortal();
        StopSecondPortal();

        if (firstPortal != null && secondPortal != null && !isTeleporting)
        {
            if (Vector2.Distance(transform.position, firstPortal.transform.position) < 1f)
            {
                Teleport(secondPortal.transform.position);
            }
            else if (Vector2.Distance(transform.position, secondPortal.transform.position) < 1f)
            {
                Teleport(firstPortal.transform.position);
            }
        }
    }

    void StopFirstPortal()
    {
        if (firstPortal != null && Vector2.Distance(firstPortal.transform.position, firstPortalDestination) <= 0.2f)
        {
            firstPortal.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    void StopSecondPortal()
    {
        if (secondPortal != null && Vector2.Distance(secondPortal.transform.position, secondPortalDestination) <= 0.2f)
        {
            secondPortal.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    void Teleport(Vector3 destination)
    {
        isTeleporting = true;
        transform.position = destination;
        Invoke("ResetTeleport", 1.5f);
    }

    void ResetTeleport()
    {
        isTeleporting = false;
    }

    void LateUpdate()
    {
        if (firstPortal != null && secondPortal != null)
        {
            Destroy(firstPortal, portalLifetime);
            Destroy(secondPortal, portalLifetime);
        }
    }
}
