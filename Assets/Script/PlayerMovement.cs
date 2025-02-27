using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 500f;
    public float dashDistance = 5f;
    public float dashDuration = 0.1f;
    public static float dashCooldown = 1.75f;
    public AudioSource audioDash;
    public saveSytem save;
    private Rigidbody2D rb;
    private bool isDashing = false;
    private bool canDash = true;

    // Input keys with default values
    public string moveUpKey = "Z";
    public string moveDownKey = "S";
    public string moveLeftKey = "Q";
    public string moveRightKey = "D";
    public string dashKey = "Space";

    private Vector2 movement; // Sauvegarde de la direction de mouvement pour le dash

    void Start()
    {
        moveUpKey = string.IsNullOrEmpty(save.UpKey) ? "Z" : save.UpKey;
        moveDownKey = string.IsNullOrEmpty(save.DownKey) ? "S" : save.DownKey;
        moveLeftKey = string.IsNullOrEmpty(save.LeftKey) ? "Q" : save.LeftKey;
        moveRightKey = string.IsNullOrEmpty(save.RightKey) ? "D" : save.RightKey;
        dashKey = string.IsNullOrEmpty(save.DashKey) ? "Space" : save.DashKey;
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = save.SpeedUpgrades != 0 ? save.PlayerMoveSpeed : 5f;
    }

    void Update()
    {
        dashCooldown = 1.75f  *(1-PlayerStats.dashCooldownR);

        moveSpeed = 6 + PlayerStats.moveSpeed ;

        // Initialisation du mouvement
        movement = Vector2.zero;

        if (Input.GetKey(GetKeyCodeFromString(moveUpKey)))
        {
            movement.y += 1;
        }
        if (Input.GetKey(GetKeyCodeFromString(moveDownKey)))
        {
            movement.y -= 1;
        }
        if (Input.GetKey(GetKeyCodeFromString(moveLeftKey)))
        {
            movement.x -= 1;
        }
        if (Input.GetKey(GetKeyCodeFromString(moveRightKey)))
        {
            movement.x += 1;
        }

        movement = movement.normalized;
        rb.velocity = movement * moveSpeed;

        // Rotation vers la position de la souris
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );
        transform.up = direction;

        // Dash
        if (Input.GetKeyDown(GetKeyCodeFromString(dashKey)) && canDash && movement != Vector2.zero)
        {
            StartCoroutine(Dash());
        }
    }

    KeyCode GetKeyCodeFromString(string key)
    {
        try
        {
            key = RemoveInvisibleChars(key.Trim());
            return (KeyCode)Enum.Parse(typeof(KeyCode), key, true);
        }
        catch
        {
            Debug.LogError("Touche invalide : '" + key + "'");
            return KeyCode.None;
        }
    }

    string RemoveInvisibleChars(string input)
    {
        return new string(Array.FindAll(input.ToCharArray(), c => !char.IsControl(c) && !char.IsWhiteSpace(c) && c < 128));
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
