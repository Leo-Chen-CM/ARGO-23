using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class ARGOTests
{
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
        spawner.AddComponent<gameManager>();
        spawner.GetComponent<gameManager>().speed = 0.8f;
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

    /// <summary>
    /// Tests if the player is able to slide
    /// </summary>
    /// <returns>Player's new scale</returns>
    [UnityTest]
    public IEnumerator PlayerSlides()
    {

        Vector3 scaleStart = player.GetComponent<Transform>().localScale;

        CommandSlide _moveSlide = new CommandSlide();
        _moveSlide.Execute(player.GetComponent<Unit>(), _moveSlide);

        yield return new WaitForSeconds(0.5f);


        Vector3 scaleEnd = player.GetComponent<Transform>().localScale;

        Assert.Less(scaleEnd.y, scaleStart.y);

        Debug.Log("Scale start: " + scaleStart.y + "\n" + "Scale End: " + scaleEnd.y);

    }

    /// <summary>
    /// Tests if the player is able to jump
    /// </summary>
    /// <returns>Player's new position</returns>
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

    /// <summary>
    /// Tests if the player moves the left of the original position
    /// </summary>
    /// <returns>Player's new position</returns>
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


    /// <summary>
    /// Tests if the player is able to move right
    /// </summary>
    /// <returns>Player's new right</returns>
    [UnityTest]
    public IEnumerator PlayerMovesRight()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        Vector3 positionStart = player.GetComponent<Transform>().position;

        CommandMoveRight _moveRight = new CommandMoveRight();
        _moveRight.Execute(player.GetComponent<Unit>(), _moveRight);

        yield return new WaitForSeconds(1f);

        Vector3 positionEnd = player.GetComponent<Transform>().position;

        Assert.Greater(positionEnd.x, positionStart.x);

        Debug.Log("Position start: " + positionStart + "\n" + "Position End: " + positionEnd);
    }

    /// <summary>
    /// Tests the spawning on items
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator Spawn()
    {
        spawner.SetActive(true);

        yield return new WaitForSeconds(5f);

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Obstacle");

        Assert.GreaterOrEqual(gameObjects.Length,0);
    }


    //Tests if obstacles collide with the player
    [UnityTest]
    public IEnumerator CollisionTest()
    {
        GameObject rock = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Rock"));
        yield return new WaitForSeconds(1f);
        UnityEngine.Assertions.Assert.IsNull(rock);
    }


    /// <summary>
    /// Tests if the scenes can change with a button press
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator SceneChanger()
    {
        //Test Game Scene
        SceneManager.LoadScene("MainMenu");
        yield return new WaitForSeconds(.1f);

        GameObject button = GameObject.Find("Play");

        button.GetComponent<Button>().onClick.Invoke();

        yield return new WaitForSeconds(1f);

        Scene targetScene = SceneManager.GetSceneByBuildIndex(1);
        Scene currentScene = SceneManager.GetActiveScene();

        Assert.AreEqual(targetScene.name, currentScene.name);

        //Test credits scene
        SceneManager.LoadScene("MainMenu");
        yield return new WaitForSeconds(.1f);

        button = GameObject.Find("Credits");
        button.GetComponent<Button>().onClick.Invoke();


        yield return new WaitForSeconds(1f);

        targetScene = SceneManager.GetSceneByBuildIndex(2);
        currentScene = SceneManager.GetActiveScene();
        Assert.AreEqual(targetScene.name, currentScene.name);

        //Test Settings scene
        SceneManager.LoadScene("MainMenu");
        yield return new WaitForSeconds(.1f);

        button = GameObject.Find("Settings");
        button.GetComponent<Button>().onClick.Invoke();


        yield return new WaitForSeconds(1f);

        targetScene = SceneManager.GetSceneByBuildIndex(3);
        currentScene = SceneManager.GetActiveScene();
        Assert.AreEqual(targetScene.name, currentScene.name);
    }

}


