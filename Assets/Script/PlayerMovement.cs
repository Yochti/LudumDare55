using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]public float moveSpeed = 5f;
    public float rotationSpeed = 500f;
    public float dashDistance = 5f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 1.75f;
    public AudioSource audioDash;
    public saveSytem save;
    private Rigidbody2D rb;
    private bool isDashing = false;
    private bool canDash = true;

    //Input
    public string moveUpKey = "Z";    // Touche par défaut pour monter
    public string moveDownKey = "S";  // Touche par défaut pour descendre
    public string moveLeftKey = "Q";  // Touche par défaut pour aller à gauche
    public string moveRightKey = "D"; // Touche par défaut pour aller à droite
    public string dashKey = "Space";

    void Start()
    {
        
        moveUpKey = save.UpKey;
        moveDownKey = save.DownKey;
        moveLeftKey = save.LeftKey;
        moveRightKey = save.RightKey;
        dashKey = save.DashKey;
        rb = GetComponent<Rigidbody2D>();
        if (save.SpeedUpgrades != 0)
        {
            moveSpeed = save.PlayerMoveSpeed;
        }
        else
        {
            moveSpeed = 5f;
            print("Test");
        }
        
    }

    void Update()
    {
        

        Vector2 movement = Vector2.zero;
        
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

         movement  = movement.normalized;
        rb.velocity = movement * moveSpeed;

        // Rotation du joueur selon la position de la souris
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );
        transform.up = direction;

        // Dash
        if (Input.GetKeyDown(GetKeyCodeFromString(dashKey)) && canDash)
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
