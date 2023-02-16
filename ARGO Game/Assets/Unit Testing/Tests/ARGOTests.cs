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

    [SetUp]
    public void Setup()
    {
        GameObject PlayerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test Player"));

        player = PlayerGameObject;
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(player.gameObject);
    }


    [Test]
    public void ARGOTestsSimpleFail()
    {

        //InputHandler

        Assert.LessOrEqual(2, 1);
    }

    [UnityTest]
    public IEnumerator PlayerMovesLeft()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        //MonoBehaviour.Instantiate(,)


        Vector3 positionStart = player.GetComponent<Transform>().position;

        CommandMoveLeft _moveLeft = new CommandMoveLeft();
        _moveLeft.Execute(player.GetComponent<Unit>(), _moveLeft);

        yield return new WaitForSeconds(1f);



        Vector3 positionEnd = player.GetComponent<Transform>().position;

        Debug.Log("Position start: " + positionStart + "\n" + "Position End: " + positionEnd);

        Assert.Less(positionEnd.x, positionStart.x);

        Debug.Log("Position start: " + positionStart + "\n" + "Position End: " + positionEnd);
    }

    [UnityTest]
    public IEnumerator P()
    {
        yield return new WaitForSeconds(1f);
    }
}


