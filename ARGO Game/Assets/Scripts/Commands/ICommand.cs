/// <summary>
/// Our Command interface. 
/// Reference used: https://pavcreations.com/command-design-pattern-for-flexible-controls-schemes/
/// Worked on by: Jack Sinnott
/// </summary>

using UnityEngine;
using UnityEngine.InputSystem;

public class ICommand : ScriptableObject
{
    public virtual void Execute(InputAction t_action, GameObject t_go = null) { }

    public virtual void FixedExecute(InputAction t_action, GameObject t_go = null) { }

}
