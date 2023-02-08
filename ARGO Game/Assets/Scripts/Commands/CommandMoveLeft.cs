/// <summary>
/// Our Move command which will be shared with player and AI bots
/// Worked on by: Jack Sinnott
/// </summary>

using UnityEngine;

public class CommandMoveLeft : ICommand
{
    public override void Execute(Transform t_unitTrans, ICommand t_com)
    {
        Move(t_unitTrans);

        InputHandler._oldCommands.Add(t_com);
    }

    public override void Move(Transform _unitTrans)
    {
        _unitTrans.Translate(new Vector3(-3.5f,0.0f,0.0f));
    }
}

