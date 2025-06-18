public class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _bonus = bonus;
        _currentCount = 0;
    }

    public override void RecordEvent()
    {
        if (_currentCount < _targetCount)
            _currentCount++;
    }

    public override bool IsComplete() => _currentCount >= _targetCount;

    public override string GetStatus() =>
        $"[{(IsComplete() ? "X" : " ")}] {GetDetails()} (Completed {_currentCount}/{_targetCount} times)";

    public override string SaveData() =>
        $"Checklist|{_name}|{_description}|{_points}|{_currentCount}|{_targetCount}|{_bonus}";

    public override void LoadData(string data)
    {
        var parts = data.Split('|');
        _currentCount = int.Parse(parts[4]);
        _targetCount = int.Parse(parts[5]);
        _bonus = int.Parse(parts[6]);
    }

    public int GetBonus() => IsComplete() && _currentCount == _targetCount ? _bonus : 0;
}
