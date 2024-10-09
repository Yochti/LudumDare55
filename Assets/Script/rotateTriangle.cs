using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateTriangle : MonoBehaviour
{
    // Start is called before the first frame update

    public float rotationSpeed = 200f;
    
    void Update()
    {
        RotateBoss(); 
    }
    
     void RotateBoss()
     {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
     }
}
