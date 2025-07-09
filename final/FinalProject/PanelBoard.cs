using System.Collections.Generic;

public class PanelBoard
{
    public List<Circuit> Circuits { get; } = new List<Circuit>();

    public void AddCircuit(Circuit circuit)
    {
        Circuits.Add(circuit);
    }
}