public class SimpleGoal : Goal
{
    private bool _completed;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _completed = false;
    }

    public override void RecordEvent() => _completed = true;

    public override bool IsComplete() => _completed;

    public override string GetStatus() => $"[{(_completed ? "X" : " ")}] {GetDetails()}";

    public override string SaveData() => $"Simple|{_name}|{_description}|{_points}|{_completed}";

    public override void LoadData(string data)
    {
        var parts = data.Split('|');
        _completed = bool.Parse(parts[4]);
    }
}
