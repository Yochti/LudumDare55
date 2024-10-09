using UnityEngine;

public class TriLaserTurret : MonoBehaviour
{
    public float rotationSpeed = 50f; // Vitesse de rotation de la tourelle


    private void Update()
    {
        RotateTurret(); // Fait tourner la tourelle
    }

    private void RotateTurret()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime); // Fait tourner la tourelle
    }


}
