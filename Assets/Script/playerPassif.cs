using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPassif : MonoBehaviour
{
    public static int pVampire;
    public static void passifVampire(int healthAdd)
    {
        if (pVampire>0)
        {
            PlayerHealth.maxHealth += healthAdd;
            pVampire--;
        }
    }
    
}
