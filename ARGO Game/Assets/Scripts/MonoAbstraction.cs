using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoAbstraction : MonoBehaviour
{
    public static MonoAbstraction Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
