using System.Collections.Generic;
using UnityEngine;

public class ActionRecorder : MonoBehaviour
{
    private readonly Stack<ICommand> _actions = new Stack<ICommand>();

    public void Record(ICommand action)
    {
        _actions.Push(action);
        action.Execute();
    }

    public void Rewind()
    {
        var action = _actions.Pop();
        action.Undo();
    }
}
