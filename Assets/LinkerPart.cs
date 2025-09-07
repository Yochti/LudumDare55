using UnityEngine;

public class LinkerPart : MonoBehaviour
{
    public float moveRadius = 12f;              // Rayon autour du joueur (augment�)
    public float moveSpeed = 5f;
    public float repositionCooldown = 3f;

    private Vector3 targetPosition;
    private float timer;
    private Transform player;

    private static int linkerCount = 0;         // Pour se disperser entre eux
    private int myIndex;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        myIndex = linkerCount;                 // Donne un index unique � cette partie
        linkerCount++;

        PickNewTarget();
    }

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.2f || timer > repositionCooldown)
        {
            PickNewTarget();
            timer = 0f;
        }
    }

    void PickNewTarget()
    {
        float angleOffset = 360f / Mathf.Max(1, linkerCount);   // R�partit autour du joueur
        float angle = myIndex * angleOffset + Random.Range(-30f, 30f); // Ajoute un peu d'al�atoire

        float radius = moveRadius * Random.Range(0.8f, 1.2f);   // L�g�re variation de distance
        float radians = angle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * radius;
        targetPosition = player.position + offset;
    }

    void OnDestroy()
    {
        linkerCount = Mathf.Max(0, linkerCount - 1); // Nettoyage propre si l'objet est d�truit
    }
}
