using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AudioTests
{
    private GameObject _AudioManager;

    [SetUp]
    public void Setup()
    {
        _AudioManager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Audio Manager"));
    }


    /// <summary>
    /// Tests if the music is working
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator AudioManagerTest()
    {
        AudioManager.Instance();
        yield return null;

        Assert.IsTrue(AudioManager.Instance() != null);
    }
}
