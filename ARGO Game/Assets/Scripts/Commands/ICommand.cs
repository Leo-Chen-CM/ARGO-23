/// <summary>
/// Our Command interface. 
/// Reference used: https://pavcreations.com/command-design-pattern-for-flexible-controls-schemes/
/// Worked on by: Jack Sinnott
/// </summary>

public abstract class ICommand
{
    protected readonly Unit Unit;

    protected ICommand(Unit unit)
    {
        Unit = unit;
    }

    public abstract void Execute();
    public abstract void Undo();
}
