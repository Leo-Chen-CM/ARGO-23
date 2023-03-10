/// <summary>
/// Our Command interface. 
/// Worked on by: Jack Sinnott
/// </summary>
/// 
using UnityEngine;


public interface ICommand 
{
    public virtual void Execute(Unit t_unit, ICommand t_com) { } // Executes the actual command and allows us to save the command registered

    public virtual void Move(Unit _unit) { }

}
