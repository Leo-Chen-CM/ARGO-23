using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform leftSpawn;
    public Transform midSpawn;
    public Transform rightSpawn;
    private float waitTime;

    public GameObject obstacle;
    public GameObject pickup;

    private Vector3[] positions;

    private void Start()
    {
        positions = new Vector3[3];
        positions[0] = leftSpawn.position;
        positions[1] = midSpawn.position;
        positions[2] = rightSpawn.position;
        if(FindObjectOfType<gameManager>())
        {
            waitTime = FindObjectOfType<gameManager>().getSpeed();
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
        yield return new WaitForSeconds(waitTime / 2.0f);
        while (true)
        {
            GameObject newObs = Instantiate(obstacle, positions[Random.Range(0, 3)] + offset, Quaternion.identity);
            newObs.GetComponent<objectMover>().speed = waitTime;
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
            GameObject newPickup = Instantiate(pickup, positions[Random.Range(0, 3)] + offset, Quaternion.identity);
            newPickup.GetComponent<objectMover>().speed = waitTime;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
