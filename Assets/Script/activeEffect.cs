using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class activeEffect : MonoBehaviour
{
    public Volume globalVolume; // Référence au Global Volume
    private Vignette vignette; // Exemple d'effet à activer

    void OnEnable()
    {
        // Vérifiez si le volume est assigné
        if (globalVolume != null)
        {
            // Essayez d'obtenir le composant Vignette du volume
            if (globalVolume.profile.TryGet<Vignette>(out vignette))
            {
                // Activez l'effet (par exemple, intensifiez le vignette)
                vignette.active = true; // Assurez-vous que l'effet est activé
                vignette.intensity.Override(1f); // Exemple de valeur pour l'intensité
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
        // Si vous souhaitez désactiver l'effet lorsque le script est désactivé
        if (globalVolume != null && vignette != null)
        {
            vignette.intensity.Override(0f); // Remettez l'intensité à 0 pour désactiver l'effet
        }
    }
}
