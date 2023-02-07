/// <summary>
/// Our Move command which will be shared with player and AI bots
/// Worked on by: Jack Sinnott
/// </summary>

using UnityEngine;

public class CommandMove : ICommand
{
    private Vector3 _direction;

    public CommandMove(Unit unit, Vector3 direction) : base(unit)
    {
        _direction = direction;
    }

    public override void Execute()
    {
        Unit.Move(_direction);
    }

    public override void Undo()
    {
        Unit.Move(_direction);
    }
}
