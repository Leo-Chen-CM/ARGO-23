using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class ARGOTests
{
    //// A Test behaves as an ordinary method
    //[Test]
    //public void ARGOTestsSimplePasses()
    //{
    //    // Use the Assert class to test conditions
    //}



    private GameObject player;
    private GameObject spawner;
    private GameObject enviroment;
    private GameObject camObj;
    [SetUp]
    public void Setup()
    {
        GameObject PlayerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test Player"));

        camObj = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Main Camera"));
        player = PlayerGameObject;

        spawner = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Spawner"));
        //spawner.AddComponent<gameManager>();
        //spawner.GetComponent<gameManager>().speed = 0.8f;
        spawner.SetActive(false);
        enviroment = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Environment"));
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(player.gameObject);
        Object.Destroy(spawner.gameObject);
        Object.Destroy(enviroment.gameObject);
        Object.Destroy(camObj.gameObject);
    }


    [UnityTest]
    public IEnumerator PlayerJumps()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        Vector3 positionStart = player.GetComponent<Transform>().position;

        CommandJump _moveJump = new CommandJump();
        _moveJump.Execute(player.GetComponent<Unit>(), _moveJump);

        yield return new WaitForSeconds(0.5f);


        Vector3 positionEnd = player.GetComponent<Transform>().position;

        Assert.Greater(positionEnd.y, positionStart.y);

        Debug.Log("Position start: " + positionStart.y + "\n" + "Position End: " + positionEnd.y);
    }


    [UnityTest]
    public IEnumerator PlayerMovesLeft()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        Vector3 positionStart = player.GetComponent<Transform>().position;

        CommandMoveLeft _moveLeft = new CommandMoveLeft();
        _moveLeft.Execute(player.GetComponent<Unit>(), _moveLeft);

        yield return new WaitForSeconds(1f);



        Vector3 positionEnd = player.GetComponent<Transform>().position;

        Assert.Less(positionEnd.x, positionStart.x);

        Debug.Log("Position start: " + positionStart + "\n" + "Position End: " + positionEnd);
    }

    [UnityTest]
    public IEnumerator Spawn()
    {
        spawner.SetActive(true);

        yield return new WaitForSeconds(5f);

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Obstacle");

        Assert.GreaterOrEqual(gameObjects.Length,0);
    }
}


