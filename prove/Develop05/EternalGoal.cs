public class EternalGoal : Goal
{
    private int _timesRecorded;

    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _timesRecorded = 0;
    }

    public override void RecordEvent() => _timesRecorded++;

    public override bool IsComplete() => false;

    public override string GetStatus() => $"[∞] {GetDetails()} (Completed {_timesRecorded} times)";

    public override string SaveData() => $"Eternal|{_name}|{_description}|{_points}|{_timesRecorded}";

    public override void LoadData(string data)
    {
        var parts = data.Split('|');
        _timesRecorded = int.Parse(parts[4]);
    }
}
