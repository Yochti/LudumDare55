using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitAnimationScript : MonoBehaviour
{

    void Start()
    {
        Invoke("DestroyGO", .4f);

    }
    void DestroyGO()
    {
        Destroy(this.gameObject);
    }

    
}
