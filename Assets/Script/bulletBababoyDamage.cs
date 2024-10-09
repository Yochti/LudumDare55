using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBababoyDamage : MonoBehaviour
{
    public int BababoyDamageInt;
    
    public  void OnHit(int numberDamage)
    {
        BababoyDamageInt += numberDamage;
    }
}
