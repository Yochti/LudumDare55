using UnityEngine;
using System.Collections;
public class PlayerSoulsCollect : MonoBehaviour
{
    public static int soulValue = 80;
    public static int recentSoulsValue;
    public AudioSource soulsSFX;
    public static bool hasTouchedCoin;
    public float countdown;
    public HealthBar cointxt;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Soul"))
        {
            Destroy(other.gameObject);
            recentSoulsValue++;
            hasTouchedCoin = true;
            countdown = 0.7f;
            soulsSFX.Play();
            
        }
    }
    
    private void Update()
    {
        
        if(hasTouchedCoin)
        {
            countdown -= Time.deltaTime;
            if(countdown <= 0)
            {
                StartCoroutine(gainCoin());
                hasTouchedCoin = false;
            }



        }
    }

    private IEnumerator gainCoin()
    {
        while(recentSoulsValue > 0)
        {
            recentSoulsValue--;
            soulValue++;
            cointxt.SoulsText.text = soulValue.ToString();

            yield return new WaitForSeconds(0.03f);

        }
    }
}
