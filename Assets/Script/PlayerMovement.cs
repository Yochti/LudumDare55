using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 500f;
    public float dashDistance = 5f;
    public float dashDuration = 0.1f;
    public static float dashCooldown = 1.8f;
    public AudioSource audioDash;
    public saveSytem save;
    private Rigidbody2D rb;
    private bool isDashing = false;
    private bool canDash = true;
    public PlayerHealth playerHealth;
    private Vector2 movement;
    private Vector2 lastPosition;
    private float distanceTraveled = 0f;
    public float triggerDistance = 10f;

    // Turret passive power-up
    public static bool hasTurretActivate = false;
    private float turretBuffTimer = 0f;
    private float turretBuffTick = 0.1f;
    private float turretMaxBuffTime = 5f;
    private float turretAttackSpeedBonus = 0f;
    private float turretBuffIntervalTimer = 0f;

    public static int healthRegenMovePassif;

    void Start()
    {
        healthRegenMovePassif = 0;
        rb = GetComponent<Rigidbody2D>();
        hasTurretActivate = false;
        moveSpeed = save.SpeedUpgrades != 0 ? save.PlayerMoveSpeed : 5f;
        lastPosition = transform.position;
    }

    void Update()
    {
        moveSpeed = PlayerStats.moveSpeed;
        dashCooldown = 1.8f * (1 - PlayerStats.dashCooldownR);

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY).normalized;

        distanceTraveled += Vector2.Distance(rb.position, lastPosition);
        lastPosition = rb.position;
        if (distanceTraveled >= triggerDistance && playerHealth != null)
        {
            playerHealth.Heal(healthRegenMovePassif);
            distanceTraveled = 0f;
        }

        // Handle turret passive buff
        if (hasTurretActivate)
        {
            if (movement == Vector2.zero)
            {
                turretBuffTimer += Time.deltaTime;
                turretBuffIntervalTimer += Time.deltaTime;

                if (turretBuffTimer <= turretMaxBuffTime && turretBuffIntervalTimer >= turretBuffTick)
                {
                    turretAtqSpeed();
                    turretBuffIntervalTimer = 0f;
                }
            }
            else if (turretAttackSpeedBonus > 0f)
            {
                resetTurretAtqSpeed();
            }
        }

        rb.velocity = movement * moveSpeed;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        transform.up = direction;

        if (Input.GetKeyDown(KeyCode.Space) && canDash && movement != Vector2.zero && !save.noDashDifficulty)
        {
            StartCoroutine(Dash());
        }
    }

    private void turretAtqSpeed()
    {
        PlayerStats.attackSpeed += 0.012f;
        turretAttackSpeedBonus += 0.012f;
    }

    private void resetTurretAtqSpeed()
    {
        PlayerStats.attackSpeed -= turretAttackSpeedBonus;
        turretAttackSpeedBonus = 0f;
        turretBuffTimer = 0f;
        turretBuffIntervalTimer = 0f;
    }

    public void ActivateTurretPassive()
    {
        hasTurretActivate = true;
        turretBuffTimer = 0f;
        turretBuffIntervalTimer = 0f;
        turretAttackSpeedBonus = 0f;
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        Vector2 dashDirection = movement;
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
