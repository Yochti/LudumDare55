using UnityEngine;
using System.Collections;

public class AllyDimitri : MonoBehaviour
{
    public GameObject portalPrefab;
    public GameObject portalPrefab1;
    public GameObject portalPrefab2;
    public float portalHeight = 2f;
    public float portalDistance = 3f;
    public float portalChangeInterval = 6f;
    private GameObject player;
    public AlliesSummon alliesState;
    public string portalState = "1";
    public portal3 p3;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 playerUp = player.transform.up;
        Vector2 targetPosition = playerPosition - playerUp * portalDistance;

        transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);

        Vector2 lookDirection = (Vector2)player.transform.position - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        portalChangeInterval -= Time.deltaTime;
        if (alliesState.dimitriStatus != "3")
        {
            if (portalChangeInterval <= 0f)
            {
                if (portalPrefab.activeSelf == true)
                {
                    portalPrefab1.SetActive(true);
                    portalPrefab.SetActive(false);
                    portalChangeInterval = 6f;

                }
                else
                {
                    portalPrefab1.SetActive(false);
                    portalPrefab.SetActive(true);
                    portalChangeInterval = 6f;
                }
            }
        }
        else
        {
            if (portalChangeInterval <= 0)
            {
                if (portalState == "1")
                {
                    portalPrefab1.SetActive(true);
                    portalPrefab.SetActive(false);
                    portalPrefab2.SetActive(false);
                    portalChangeInterval = 6f;
                    portalState = "2";
                }
                else if (portalState == "2")
                {
                    portalPrefab1.SetActive(false);
                    portalPrefab.SetActive(true);
                    portalPrefab2.SetActive(false);
                    portalChangeInterval = 2.5f;
                    portalState = "3";
                }
                else if (portalState == "3")
                {
                    StartCoroutine(waitTill3Bullet());
                    p3.numberOfBullets = 0;
                    portalState = "1";
                }
            }
            
        }

    }

    public IEnumerator waitTill3Bullet()
    {
        while (p3.numberOfBullets < 3)
        {
            portalPrefab1.SetActive(false);
            portalPrefab.SetActive(false);
            portalPrefab2.SetActive(true);
            yield return null;
        }
        
    }
}
