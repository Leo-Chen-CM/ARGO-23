/// <summary>
/// Our jump command which will be shared with player and AI bots
/// Worked on by: Jack Sinnott
/// </summary>

using UnityEngine;

public class CommandJump : ICommand
{
   
    public void Execute(Unit t_unit, ICommand t_com)
    {
        Move(t_unit);

        InputHandler._oldCommands.Add(t_com);

    }

    public void Move(Unit _unit)
    {
        if (_unit.IsGrounded())
        {
            //_unit.transform.Translate(new Vector3(0, 5f, 0.0f));
            _unit._rb.velocity = Vector2.up * _unit.GetJumpForce();
        }
    }
}
