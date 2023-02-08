/// <summary>
/// Our Command interface. 
/// Worked on by: Jack Sinnott
/// </summary>
/// 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ICommand
{
    public abstract void Execute(Transform t_unitTrans, ICommand t_com); // Executes the actual command and allows us to save the command registered

    public virtual void Move(Transform _unitTrans) { }

}
