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

    Vector3 offset;



    [SerializeField] public GameObject[] obstacles;
    [SerializeField] public GameObject[] pickups;
    [SerializeField] public GameObject Coin;
    
    private Vector3[] positions;

    [Server]
    public override void OnStartServer()
    {
        positions = new Vector3[6];
        positions[0] = leftSpawn.position;
        positions[1] = midSpawn.position;
        positions[2] = rightSpawn.position;
        positions[3] = upleftSpawn.position;
        positions[4] = upmidSpawn.position;
        positions[5] = uprightSpawn.position;

        offset = obstacles[0].GetComponent<Renderer>().bounds.size;
        offset.x = 0;
        offset.y /= 2;
        offset.z = 0;
        if (FindObjectOfType<gameManager>())
        {
            speed = FindObjectOfType<gameManager>().getSpeed();
            waitTime = 1.0f / speed;
        }

        StartCoroutine(spawnObstacles());
    }

    [Server]
    private IEnumerator spawnObstacles()
    {
        yield return new WaitForSeconds(waitTime / 2.0f);
        while (true)
        {
            int temp = Random.Range(0, obstacles.Length);
            GameObject newObs = Instantiate(obstacles[temp], positions[Random.Range(0, 3)] + offset, Quaternion.identity);
            Debug.Log("current Random number for obstacles: " + temp);
            if(temp == 0) newObs.GetComponent<obstacleObject>().speed = speed;
            else if(temp == 1) newObs.GetComponent<SpiderScript>().speed = speed;
            newObs.gameObject.transform.SetParent(this.transform);
            NetworkServer.Spawn(newObs);
            yield return new WaitForSeconds(waitTime);
        }
    }
    [Server]
    private IEnumerator spawnPickups()
    {
        while(true)
        {
            int temp = Random.Range(0, pickups.Length);
            GameObject newPickup = Instantiate(pickups[temp], positions[Random.Range(4, 6)] + offset, Quaternion.identity);
            Debug.Log("current Random number for pick ups: " + temp);
            newPickup.GetComponent<CollectableObject>().speed = speed;
            newPickup.gameObject.transform.SetParent(this.transform);
            NetworkServer.Spawn(newPickup);
            waitTime = 1.5f;
            yield return new WaitForSeconds(waitTime);
        }
    }

}
