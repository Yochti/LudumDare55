using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RetryButtonn : MonoBehaviour
{
    public GameObject panelDeath;
    public EnemmiHealth eHP;
    public EnemmiHealth eHP2;
    public EnemmiHealth eHP3;
    public EnemyAI e1;
    public EnemyAI3 e3;
    public BulletLifEnemy eBullet;
    private void Start()
    {
        panelDeath.SetActive(false);

    }
    public void retry()
    {
        SceneManager.LoadScene(1);
        panelDeath.SetActive(false);
        Score.ScoreCount = 0;
        PlayerSoulsCollect.soulValue = 80;
        eHP.maxHealth = 80;
        eHP2.maxHealth = 50;
        eHP3.maxHealth = 25;
        e1.damage = 10f;
        e3.explosionDamage = 25f;
        eBullet.bulletDamage = 8;


    }
    private void Update()
    {
        bool isActive = !panelDeath.activeSelf;
        if (!isActive)
        {
            Time.timeScale = 0f;

        }

    }


}
