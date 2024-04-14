using UnityEngine;

public class PlayerSoulsCollect : MonoBehaviour
{
    public int soulValue = 1;
    public AudioSource soulsSFX;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Soul"))
        {
            soulValue++;
            soulsSFX.Play();
            Destroy(other.gameObject);
        }
    }
}
