using System.Collections;
using UnityEngine;

public class CommandSlide : ICommand
{
    bool _sliding = false;
    public void Execute(Unit t_unit, ICommand t_com)
    {
        Move(t_unit);

        InputHandler._oldCommands.Add(t_com);
    }

    public void Move(Unit _unit)
    {
        if (!_sliding)
        {
            _unit.transform.localScale = new Vector3(0.3f, .1f, 1);
            
            _sliding = !_sliding;
        }
        else if (_sliding)
        {
            _unit.transform.localScale = new Vector3(.3f, .3f, 1);
            _sliding = !_sliding;
        }
    }
}
