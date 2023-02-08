/// <summary>
/// Our Move command which will be shared with player and AI bots
/// Worked on by: Jack Sinnott
/// </summary>

using UnityEngine;

public class CommandMoveRight : ICommand
{
    private float _rightBound = 3.5f;
    public override void Execute(Unit t_unit, ICommand t_com)
    {
        Move(t_unit);

        InputHandler._oldCommands.Add(t_com);
    }

    public override void Move(Unit _unit)
    {
        if (_unit.transform.position.x < _rightBound)
            _unit.transform.Translate(new Vector3(3.5f, 0.0f, 0.0f));
        //_unit._rb.velocity -= new Vector3(3.5f, 0.0f, 0.0f);
    }
}
