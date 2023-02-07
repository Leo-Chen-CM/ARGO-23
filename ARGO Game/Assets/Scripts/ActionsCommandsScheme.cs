using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Controls/ActionsCommandsScheme")]
public class ActionsCommandsScheme : ScriptableObject
{
    public List<ActionCommandPair> actionCommandList;
}
