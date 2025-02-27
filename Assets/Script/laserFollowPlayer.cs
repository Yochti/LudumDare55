using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserFollowPlayer : MonoBehaviour
{
    public GameObject player;
    
    void Update()
    {
        this.gameObject.transform.position = player.transform.position;
    }
}
