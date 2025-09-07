using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementPlayerMenu : MonoBehaviour
{
    public Rigidbody2D rb;
    private void Update()
    {
        rb.velocity = new Vector2(0,1);
    }
}
