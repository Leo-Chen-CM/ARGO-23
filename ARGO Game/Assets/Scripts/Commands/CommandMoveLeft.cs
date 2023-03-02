/// <summary>
/// Our Move command which will be shared with player and AI bots
/// Worked on by: Jack Sinnott
/// </summary>

using UnityEngine;

public class CommandMoveLeft : ICommand
{
    private float _leftBound = -3.4f;
    public void Execute(Unit t_unit, ICommand t_com)
    {
        Move(t_unit);

        InputHandler._oldCommands.Add(t_com);
    }

    public void Move(Unit _unit)
    {
        if(_unit.transform.position.x > _leftBound)
            _unit.transform.Translate(new Vector3(-3.5f,0.0f,0.0f));

    }
}

