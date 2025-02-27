using UnityEngine;
using UnityEngine.UI; // Nécessaire pour utiliser UI Image

public class PortalManager : MonoBehaviour
{
    public GameObject portalPrefab; // Prefab du portail
    public float shortRange = 5f; // Portée du premier portail
    public float longRange = 20f; // Portée du deuxième portail
    public float portalSpeed = 10f;
    public float portalLifetime = 10f;
    public float baseCooldown = 20f;
    private float initialCooldown = 20f;
    public Image cooldownImage;
    public Sprite cooldownSprite;
    public GameObject Player;
    private GameObject firstPortal; // Premier portail tiré
    private GameObject secondPortal; // Deuxième portail tiré
    private bool isTeleporting = false; // Indique si le joueur est en train de se téléporter
    private bool isFirstPortal = true;
    private Vector3 firstPortalDestination; // Destination du premier portail
    private Vector3 secondPortalDestination; // Destination du deuxième portail
    private float portalCountdown; // Cooldown actuel

    private void Start()
    {
        cooldownImage.sprite = cooldownSprite;
        portalCountdown = 0; // Initialiser le cooldown
        cooldownImage.fillAmount = 1 - (portalCountdown / initialCooldown);
    }

    void Update()
    {
        initialCooldown = 20 * (1 - PlayerStats.superCooldownR);
        if (portalCountdown > 0)
        {
            portalCountdown -= Time.deltaTime;
            cooldownImage.fillAmount = 1 - (portalCountdown / baseCooldown); // Met à jour le remplissage de l'image
        }

        // Vérifie les entrées de l'utilisateur pour activer les portails
        if (Input.GetMouseButtonDown(1) && !isTeleporting)
        {
            if (portalCountdown <= 0)
            {
                if (isFirstPortal)
                {
                    CreateFirstPortal();
                }
                else
                {
                    CreateSecondPortal();
                    portalCountdown = initialCooldown; // Réinitialiser le cooldown après avoir créé le deuxième portail
                }
            }
        }

        StopFirstPortal();
        StopSecondPortal();

        HandleTeleportation();
    }

    private void CreateFirstPortal()
    {
        Vector3 mousePosition1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        firstPortal = Instantiate(portalPrefab, transform.position, Quaternion.identity);
        firstPortalDestination = transform.position + (mousePosition1 - transform.position).normalized * shortRange;
        Vector3 firstPortalVelocity = (firstPortalDestination - firstPortal.transform.position).normalized * portalSpeed;
        firstPortal.GetComponent<Rigidbody2D>().velocity = firstPortalVelocity;
        isFirstPortal = false;
    }

    private void CreateSecondPortal()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        secondPortal = Instantiate(portalPrefab, transform.position, Quaternion.identity);
        secondPortalDestination = transform.position + (mousePosition - transform.position).normalized * longRange;
        Vector3 secondPortalVelocity = (secondPortalDestination - secondPortal.transform.position).normalized * portalSpeed;
        secondPortal.GetComponent<Rigidbody2D>().velocity = secondPortalVelocity;

        LateUpdate();
        isFirstPortal = true;
    }

    private void HandleTeleportation()
    {

        if (firstPortal != null && secondPortal != null && !isTeleporting)
        {
            float distanceToFirst = Vector2.Distance(Player.transform.position, firstPortal.transform.position);
            float distanceToSecond = Vector2.Distance(Player.transform.position, secondPortal.transform.position);

            if (distanceToFirst < 1.5f)
            {
                Teleport(secondPortal.transform.position);
            }
            else if (distanceToSecond < 1.5f)
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
        Player.transform.position = destination;
        Invoke("ResetTeleport", 1.5f);
    }

    void ResetTeleport()
    {
        isTeleporting = false;
    }

    void LateUpdate()
    {
        if (firstPortal != null)
        {
            Destroy(firstPortal, portalLifetime);
        }
        if (secondPortal != null)
        {
            Destroy(secondPortal, portalLifetime);
        }
    }
}
