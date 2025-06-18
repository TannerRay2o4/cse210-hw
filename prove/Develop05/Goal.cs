public abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetStatus();
    public abstract string SaveData();
    public abstract void LoadData(string data);

    public virtual string GetName() => _name;
    public virtual int GetPoints() => _points;

    public virtual string GetDetails() => $"{_name}: {_description}";
}
