using System.Collections.Generic;

public class Circuit
{
    public int BreakerSize { get; }
    public List<ElectricalRun> Components { get; }

    public Circuit(int breakerSize)
    {
        BreakerSize = breakerSize;
        Components = new List<ElectricalRun>();
    }

    public void AddComponent(ElectricalRun run)
    {
        Components.Add(run);
    }
}