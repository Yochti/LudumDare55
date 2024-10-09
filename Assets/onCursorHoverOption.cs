using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onCursorHoverOption : MonoBehaviour
{
    public Image image;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Cursor"))
        {
            image.color = Color.black;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        image.color = Color.HSVToRGB(15, 19, 21);

    }
}
