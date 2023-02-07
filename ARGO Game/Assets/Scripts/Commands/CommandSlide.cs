/// <summary>
/// Our slide command which will be shared with player and AI bots
/// Worked on by: Jack Sinnott
/// </summary>

using UnityEngine;
using UnityEngine.InputSystem;

public class CommandSlide : ICommand
{
    GameObject _gameObject;
    Vector3 _direction;

    public CommandSlide(GameObject t_obj, Vector3 t_dir)
    {
        _gameObject = t_obj;
        _direction = t_dir;
    }

    public override void Execute(InputAction t_action, GameObject t_go = null)
    {
        _gameObject.transform.position += _direction;
    }

    public override void FixedExecute(InputAction t_action, GameObject t_go = null)
    {

    }
}
