using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMove : ICommand
{
    GameObject _gameObject;
    Vector3 _direction;

    public CommandMove(GameObject t_obj, Vector3 t_dir)
    {
        _gameObject = t_obj;
        _direction = t_dir;
    }

    public void Execute()
    {
        _gameObject.transform.position += _direction;
    }
}
