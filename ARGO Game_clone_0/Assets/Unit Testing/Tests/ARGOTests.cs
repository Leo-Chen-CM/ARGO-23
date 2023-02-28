using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;


public class ARGOTests
{
    private GameObject player;
    private GameObject spider;
    private GameObject spawner;
    private GameObject enviroment;
    private GameObject camObj;
    private GameObject network;
    private GameObject PlayerGameObject;
    private GameObject SpiderGameObject;

    [SetUp]
    public void Setup()
    {
        PlayerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/lidia_0"));
       
        camObj = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Main Camera"));

        player = PlayerGameObject;

      
        spawner = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Spawner"));

        spawner.AddComponent<gameManager>();

        spawner.GetComponent<gameManager>().speed = 0.8f;

        spawner.SetActive(false);

        enviroment = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Environment"));

        network = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Network Manager"));
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

        ICommand _moveSlide = new CommandSlide();

        _moveSlide.Execute(player.GetComponent<Unit>(), _moveSlide);

        yield return new WaitForSeconds(1f);


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

        ICommand _moveJump = new CommandJump();

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

    //Tests if obstacles collide with the player
    [UnityTest]
    public IEnumerator CollisionTest()
    {
        GameObject rock = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Rock"));
        yield return new WaitForSeconds(1f);
        UnityEngine.Assertions.Assert.IsNull(rock);
    }

    /// <summary>
    /// see if spider moves
    /// </summary>
    /// <returns></returns>
    [UnityTest]

    public IEnumerator SpiderMoves()
    {
        SpiderGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/SpiderTest"));

        SpiderGameObject.transform.position = new Vector3(1.4f,-0.4f,53.0f);

        Vector3 positionStart = SpiderGameObject.GetComponent<Transform>().position;
   
        yield return new WaitForSeconds(1f);

        Vector3 positionEnd = SpiderGameObject.GetComponent<Transform>().position;

        Assert.Less(positionEnd.z, positionStart.z);

       
    }

    /// <summary>
    /// see if Rat moves
    /// </summary>
    /// <returns></returns>
    [UnityTest]

    public IEnumerator RatMoves()
    {
        GameObject RatGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/rat_2_Brighter"));

        RatGameObject.transform.position = new Vector3(3.4f, -0.4f, 63.0f);

        Vector3 positionStart = RatGameObject.GetComponent<Transform>().position;

        yield return new WaitForSeconds(1f);

        Vector3 positionEnd = RatGameObject.GetComponent<Transform>().position;

        Assert.Less(positionEnd.z, positionStart.z);


    }

    /// <summary>
    /// see if Lava pickup moves
    /// </summary>
    /// <returns></returns>
    [UnityTest]

    public IEnumerator LavaPickupMoves()
    {
        GameObject LavaPickup = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/lavaPickUp"));

        LavaPickup.transform.position = new Vector3(3.4f, -0.4f, 63.0f);

        Vector3 positionStart = LavaPickup.GetComponent<Transform>().position;

        yield return new WaitForSeconds(1f);

        Vector3 positionEnd = LavaPickup.GetComponent<Transform>().position;

        Assert.Less(positionEnd.z, positionStart.z);


    }

    /// <summary>
    /// see if Shield pickup moves
    /// </summary>
    /// <returns></returns>
    [UnityTest]

    public IEnumerator ShieldPickupMoves()
    {
        GameObject ShieldPickup = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/shieldIcon"));

        ShieldPickup.transform.position = new Vector3(3.4f, -0.4f, 73.0f);

        Vector3 positionStart = ShieldPickup.GetComponent<Transform>().position;

        yield return new WaitForSeconds(1f);

        Vector3 positionEnd = ShieldPickup.GetComponent<Transform>().position;

        Assert.Less(positionEnd.z, positionStart.z);


    }

    /// <summary>
    /// see if Rock pickup moves
    /// </summary>
    /// <returns></returns>
    [UnityTest]

    public IEnumerator RockMoves()
    {
        GameObject Rock = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Rock"));

        Rock.transform.position = new Vector3(3.4f, -0.4f, 73.0f);

        Vector3 positionStart = Rock.GetComponent<Transform>().position;

        yield return new WaitForSeconds(1f);

        Vector3 positionEnd = Rock.GetComponent<Transform>().position;

        Assert.Less(positionEnd.z, positionStart.z);


    }

    /// <summary>
    /// see if coin pickup moves
    /// </summary>
    /// <returns></returns>
    [UnityTest]

    public IEnumerator coinMoves()
    {
        GameObject Coin = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/goldCoin1"));

        Coin.transform.position = new Vector3(3.4f, -0.4f, 73.0f);

        Vector3 positionStart = Coin.GetComponent<Transform>().position;

        yield return new WaitForSeconds(1f);

        Vector3 positionEnd = Coin.GetComponent<Transform>().position;

        Assert.Less(positionEnd.z, positionStart.z);


    }

    /// <summary>
    /// see if web gets spawned
    /// </summary>
    /// <returns></returns>
    [UnityTest]

    public IEnumerator WebSpawn()
    {
        GameObject web = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/SpiderWeb"));

        web.transform.position = new Vector3(2.4f,-0.4f,29.0f);

     
        yield return new WaitForSeconds(1f);

        Assert.Less(web.transform.position.z,143.0f);

    }


    /// <summary>
    /// see if Lava  gets spawned  by comparing their posistions
    /// </summary>
    /// <returns></returns>
    [UnityTest]

    public IEnumerator LavaSpawn()
    {
        GameObject Player = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/lidia_0"));
        GameObject Lava = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Lava"));
        GameObject LavaPickup = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/lavaPickUp"));

        LavaPickup.transform.position = new Vector3(2.4f, -0.4f, 29.0f);



        yield return new WaitForSeconds(2f);



        Assert.Less(Lava.transform.position.z, 80.0f);


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


