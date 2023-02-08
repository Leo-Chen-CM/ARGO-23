using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMoveRight : ICommand
{
    public override void Execute(Transform t_unitTrans, ICommand t_com)
    {
        Move(t_unitTrans);

        InputHandler._oldCommands.Add(t_com);
    }

    public override void Move(Transform _unitTrans)
    {
        _unitTrans.Translate(new Vector3(3.5f, 0.0f, 0.0f));
    }
}
