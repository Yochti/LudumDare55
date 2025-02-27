using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class globalVolumeDeath : MonoBehaviour
{
    public Volume gVDeath;
    private Vignette vignetteDeath;
    public GameObject deathPanel;
    void Start()
    {
        if (gVDeath.profile.TryGet<Vignette>(out vignetteDeath))
        {
            vignetteDeath.active = false;
        }
    }
    private void Update()
    {
        if(deathPanel.activeSelf)
        {
            vignetteDeath.active = true;
        }
            
        
    }
}
