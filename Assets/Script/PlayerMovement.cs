using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 500f;
    public float dashDistance = 5f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 2f;
    public AudioSource audioDash;

    private Rigidbody2D rb;
    private bool isDashing = false;
    private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Déplacement du joueur
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * moveSpeed;

        // Rotation du joueur selon la position de la souris
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );
        transform.up = direction;

        // Dash
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        Vector2 startPosition = transform.position;
        Vector2 dashDirection = transform.up; // Utilise la direction vers laquelle le joueur regarde
        float startTime = Time.time;
        audioDash.Play();
        while (Time.time < startTime + dashDuration)
        {
            transform.Translate(dashDirection * (dashDistance / dashDuration) * Time.deltaTime, Space.World);
            yield return null;
        }

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
