using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// By declaring our map as a class we can query it in inspector which will be handy for debugging
/// </summary>
[System.Serializable]
public class ActionCommandPair
{
    public InputAction _key;
    public ICommand _val;
}

/// <summary>
/// Our inputHandler that allows us to associate inputs with commands.
/// We have an inverse map to query the input list through button pressed or command executed
/// </summary>
public class InputHandler : MonoBehaviour
{
    List<ActionCommandPair> actionCommandList = new List<ActionCommandPair>();
    public Dictionary<InputAction, ICommand> bindActions = new Dictionary<InputAction, ICommand>();
    public Dictionary<ICommand, InputAction> reversedBindActions = new Dictionary<ICommand, InputAction>();

    GameObject player;

    #region Singleton
    public static InputHandler instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        foreach (var action in bindActions)
            action.Value.Execute(action.Key, player);
    }

    void FixedUpdate()
    {
        foreach (var action in bindActions)
            action.Value.FixedExecute(action.Key, player);
    }

    void OnEnable()
    {
        UpdateActionsCommandsBindings();
    }

    void OnDisable()
    {
        foreach (var action in bindActions)
            action.Key.Disable();
    }

    public void UpdateActionsCommandsBindings()
    {
        bindActions.Clear();
        reversedBindActions.Clear();
        foreach (var acp in actionCommandList)
        {
            bindActions[acp._key] = acp._val;
            reversedBindActions[acp._val] = acp._key;
            acp._key.Enable();
        }
    }

    public void UpdateActionsCommandsList(List<ActionCommandPair> aList)
    {
        actionCommandList = aList;
    }
}

