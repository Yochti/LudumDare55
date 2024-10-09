using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    private float destroyDelay = 2f;
    void Start()
    {
        Destroy(this.gameObject, destroyDelay);
    }

    
}
