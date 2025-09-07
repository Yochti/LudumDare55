using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class activeEffect : MonoBehaviour
{
    public Volume globalVolume; // R�f�rence au Global Volume
    private Vignette vignette; // Exemple d'effet � activer

    void OnEnable()
    {
        // V�rifiez si le volume est assign�
        if (globalVolume != null)
        {
            // Essayez d'obtenir le composant Vignette du volume
            if (globalVolume.profile.TryGet<Vignette>(out vignette))
            {
                // Activez l'effet (par exemple, intensifiez le vignette)
                vignette.active = true; // Assurez-vous que l'effet est activ�
                vignette.intensity.Override(1f); // Exemple de valeur pour l'intensit�
            }
            else
            {
                Debug.LogWarning("Vignette effect not found in the volume profile.");
            }
        }
        else
        {
            Debug.LogError("Global Volume is not assigned!");
        }
    }

    void OnDisable()
    {
        // Si vous souhaitez d�sactiver l'effet lorsque le script est d�sactiv�
        if (globalVolume != null && vignette != null)
        {
            vignette.intensity.Override(0f); // Remettez l'intensit� � 0 pour d�sactiver l'effet
        }
    }
}
