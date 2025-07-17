using System.Collections.Generic;

public class Circuit
{
    public int BreakerSize { get; private set; }
    public List<ElectricalRun> Components { get; private set; }

    public Circuit()
    {
        BreakerSize = 20; 
        Components = new List<ElectricalRun>();
    }

    public Circuit(int breakerSize)
    {
        BreakerSize = breakerSize;
        Components = new List<ElectricalRun>();
    }

    public void AddComponent(ElectricalRun run)
    {
        Components.Add(run);
    }

    public double TotalWireLength()
    {
        double total = 0;
        foreach (var run in Components)
            total += run.WireLength;
        return total;
    }
}
