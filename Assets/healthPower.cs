using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPower : MonoBehaviour
{
    public PlayerHealth pH;
    public int healA;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            pH.Heal(healA);
            Destroy(this.gameObject);
        }
    }
}
