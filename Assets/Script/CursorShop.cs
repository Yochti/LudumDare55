using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorShop : MonoBehaviour
{   
    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }
}
