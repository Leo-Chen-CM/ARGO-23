using System.Collections;
using UnityEngine;

public class CommandSlide : ICommand
{
    public void Execute(Unit t_unit, ICommand t_com)
    {
        Move(t_unit);

        InputHandler._oldCommands.Add(t_com);
    }

    public void Move(Unit _unit)
    {
        MonoAbstraction.Instance.StartCoroutine(PleaseSlideForTheLoveOfGod(_unit));
    }

    private IEnumerator PleaseSlideForTheLoveOfGod(Unit _unit)
    {
        _unit.transform.localScale = new Vector3(3f, 1f, 1f);

        yield return new WaitForSeconds(1f);
        
        _unit.transform.localScale = new Vector3(3f, 3f, 1f);

    }
}
