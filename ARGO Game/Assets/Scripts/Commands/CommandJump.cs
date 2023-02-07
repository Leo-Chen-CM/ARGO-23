/// <summary>
/// Our jump command which will be shared with player and AI bots
/// Worked on by: Jack Sinnott
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandJump : ICommand
{
    GameObject _gameObject;
    Vector3 _direction;

    public CommandJump(GameObject t_obj, Vector3 t_dir)
    {
        _gameObject = t_obj;
        _direction = t_dir;
    }

    public void Execute()
    {
        _gameObject.transform.position += _direction;
    }
}
