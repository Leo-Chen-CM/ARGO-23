using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : NetworkBehaviour
{
    /// the left lane spawn location
    public Transform leftSpawn;
    /// the mid lane spawn location
    public Transform midSpawn;
    /// the right lane spawn location
    public Transform rightSpawn;

    private float coinTime;
    private float obstacleTime;
    private float pickUpTime;
    private float speed;

    private int numberOfCoinSpawned = 0;

    Vector3 offset;

    bool coinsSet = false;
    int getLaneToSpawn;
    int maxCoinsToSpawn = 0;

    /// the obstacles that can spawn
    [SerializeField] public GameObject[] obstacles;
    /// the pickups that can spawn
    [SerializeField] public GameObject[] pickups;
    /// the coin object
    [SerializeField] public GameObject Coin;
    
    private Vector3[] positions;

    /// <summary>
    /// this is called when the mirror server starts
    /// </summary>
    [Server]
    public override void OnStartServer()
    {
        positions = new Vector3[3];
        positions[0] = leftSpawn.position;
        positions[1] = midSpawn.position;
        positions[2] = rightSpawn.position;

        offset = obstacles[0].GetComponent<Renderer>().bounds.size;
        offset.x = 0;
        offset.y /= 2;
        offset.z = 0;
        if (FindObjectOfType<gameManager>())
        {
            speed = FindObjectOfType<gameManager>().getSpeed();
            obstacleTime = 1.0f / speed;
            coinTime = .2f / speed;
            pickUpTime = 3.0f / speed;
        }

        StartCoroutine(spawnObstacles());
        StartCoroutine(spawnPickups());
        StartCoroutine(spawnCoins());
    }

    [Server]
    private IEnumerator spawnObstacles()
    {
        yield return new WaitForSeconds(obstacleTime / 2.0f);
        while (true)
        {
            int getRandomObstacle = Random.Range(0, obstacles.Length);
            GameObject newObs = Spawn(obstacles[getRandomObstacle], Random.Range(0, 3));
            if(getRandomObstacle == 0) newObs.GetComponent<obstacleObject>().speed = speed;
            else if(getRandomObstacle == 1) newObs.GetComponent<SpiderScript>().speed = speed;
            else if (getRandomObstacle == 2) newObs.GetComponent<BatScript>().speed = speed;
            else if (getRandomObstacle == 3) newObs.GetComponent<HoleScript>().speed = speed;

            NetworkServer.Spawn(newObs);
            yield return new WaitForSeconds(obstacleTime);
        }
    }

    [Server]
    private IEnumerator spawnPickups()
    {
        while(true)
        {
            int getRandomPickUp = Random.Range(0, pickups.Length);
            GameObject newPickup = Spawn(pickups[getRandomPickUp], Random.Range(0, 3));
         
            if(getRandomPickUp == 0) newPickup.GetComponent<LavaPickupScript>().speed = speed;
            if(getRandomPickUp == 1) newPickup.GetComponent<shielScript>().speed = speed;

            NetworkServer.Spawn(newPickup);

            yield return new WaitForSeconds(pickUpTime);
        }
    }

    [Server]
    private IEnumerator spawnCoins()
    {
        while (true)
        {
            if (!coinsSet) // if we haven't established how many coins to spawn do this before anything else
            {
                maxCoinsToSpawn = Random.Range(4, 10);
                coinsSet = true;
            }
                
            if (numberOfCoinSpawned == 0) getLaneToSpawn = Random.Range(0, 3); // If we haven't started spawning the coins pick a lane to spawn them all in


            GameObject newPickup = Spawn(Coin, getLaneToSpawn); // spawn our coin in this lane


            newPickup.GetComponent<CollectableObject>().speed = speed;

            NetworkServer.Spawn(newPickup);
            numberOfCoinSpawned += 1;

            if(coinTime == 4f)
            {
                coinTime = .2f / speed;
            }

            if (numberOfCoinSpawned == maxCoinsToSpawn)
            {
                numberOfCoinSpawned = 0;
                coinTime = 4f;
                coinsSet = false;
            }
            yield return new WaitForSeconds(coinTime);
            
        }
    }

    private GameObject Spawn(GameObject t_objectPrefab, int t_lane)
    {
        GameObject newPickup = Instantiate(t_objectPrefab, positions[t_lane] + offset, Quaternion.identity);
        newPickup.gameObject.transform.SetParent(this.transform);
        return newPickup;
    }
}
