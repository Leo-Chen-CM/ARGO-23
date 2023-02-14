using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : NetworkBehaviour
{
    public Transform leftSpawn;
    public Transform midSpawn;
    public Transform rightSpawn;

    public Transform upleftSpawn;
    public Transform upmidSpawn;
    public Transform uprightSpawn;
    private float waitTime;
    private float speed;
    public int SpiderCount = 0;
    public int ShieldCount = 0;



    [SerializeField] public GameObject obstacle;
    [SerializeField] public GameObject pickup;
    [SerializeField] public GameObject Spider;
    [SerializeField] public GameObject Shield;


    private Vector3[] positions;

    public override void OnStartServer()
    {
        positions = new Vector3[6];
        positions[0] = leftSpawn.position;
        positions[1] = midSpawn.position;
        positions[2] = rightSpawn.position;
        positions[3] = upleftSpawn.position;
        positions[4] = upmidSpawn.position;
        positions[5] = uprightSpawn.position;
        if (FindObjectOfType<gameManager>())
        {
            speed = FindObjectOfType<gameManager>().getSpeed();
            waitTime = 1.0f / speed;
        }

        StartCoroutine(spawnObstacles());
    }

    private IEnumerator spawnObstacles()
    {
        Vector3 offset = obstacle.GetComponent<Renderer>().bounds.size;
        offset.x = 0;
        offset.y /= 2;
        offset.z = 0;
        StartCoroutine(spawnPickups());
        //StartCoroutine(spawnSpider());
        StartCoroutine(spawnShield());
        yield return new WaitForSeconds(waitTime / 2.0f);
        while (true)
        {
            GameObject newObs = Instantiate(obstacle, positions[Random.Range(0, 3)] + offset, Quaternion.identity);
            newObs.GetComponent<obstacleObject>().speed = speed;
            newObs.gameObject.transform.SetParent(this.transform);
            NetworkServer.Spawn(newObs);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator spawnPickups()
    {
        Vector3 offset = pickup.GetComponent<Renderer>().bounds.size;
        offset.x = 0;
        offset.y /= 2;
        offset.z = 0;
        while(true)
        {
            GameObject newPickup = Instantiate(pickup, positions[Random.Range(4, 6)] + offset, Quaternion.identity);
            newPickup.GetComponent<CollectableObject>().speed = speed;
            newPickup.gameObject.transform.SetParent(this.transform);
            NetworkServer.Spawn(newPickup);
            yield return new WaitForSeconds(waitTime);
        }
    }



    private IEnumerator spawnSpider()
    {
        Vector3 offset = pickup.GetComponent<Renderer>().bounds.size;
        offset.x = 0;
        offset.y /= 2;
        offset.z = 0;

        while (SpiderCount<1)
        {
            
            GameObject NewSpider = Instantiate(Spider, positions[1] + offset, Quaternion.identity);
            NetworkServer.Spawn(NewSpider);
            yield return new WaitForSeconds(waitTime);
            SpiderCount = 1;
            Debug.Log("spider count " + SpiderCount);
        }
    }

    private IEnumerator spawnShield()
    {
        Vector3 offset = pickup.GetComponent<Renderer>().bounds.size;
        offset.x = 0;
        offset.y /= 2;
        offset.z = 0;

        while (ShieldCount < 1)
        {

            GameObject NewShield = Instantiate(Shield, positions[1] + offset, Quaternion.identity);
            NetworkServer.Spawn(NewShield);
            yield return new WaitForSeconds(waitTime);
            ShieldCount = 1;
            Debug.Log("ShieldCount " + ShieldCount);
        }
    }

}
