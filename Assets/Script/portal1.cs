using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal1 : MonoBehaviour
{
    private BulletPlayerLifetime BulletS;
    private spikeBullet BulletSp;

    //private GameObject BulletG;
    public bool hasPassedThePortal = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            BulletS = collision.GetComponent<BulletPlayerLifetime>();
            //BulletG = collision.gameObject;
            BulletS.bulletDamage *= 2;
            print("les dmg apres portails sont:" + BulletS.bulletDamage);
            hasPassedThePortal = true;
        }if (collision.CompareTag("BulletS"))
        {
            BulletSp = collision.GetComponent<spikeBullet>();
            //BulletG = collision.gameObject;
            BulletSp.bulletDamagemin *= 2;
            BulletSp.bulletDamagemax *= 2;
            hasPassedThePortal = true;
        }
    }
}
