using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public Sprite defaultCursorSprite; // Sprite du curseur par défaut
    public Sprite hoverEnemyBossSprite; // Sprite du curseur lorsque survolant un ennemi ou un boss
    public Sprite cursorOutGame;
    private SpriteRenderer rend;

    private void Start()
    {
        Cursor.visible = false;
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = defaultCursorSprite; 
    }

    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;


    }
    /*public void changeToCursorOutgame()
    {
       
        rend.sprite = cursorOutGame;
    }*/
    public void resetCursor()
    {
        rend.sprite = defaultCursorSprite;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            rend.sprite = hoverEnemyBossSprite;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        rend.sprite = defaultCursorSprite;

    }


}
