using System;
using System.Collections.Generic;
using System.IO;

public class PanelBoard
{
    public List<Circuit> Circuits { get; set; } = new List<Circuit>();

    public void AddCircuit(Circuit circuit)
    {
        Circuits.Add(circuit);
    }

    public static PanelBoard LoadFromFile(string filename)
    {
        PanelBoard panel = new PanelBoard();

        if (!File.Exists(filename))
            return panel;

        var lines = File.ReadAllLines(filename);

        Circuit currentCircuit = null;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            if (line.StartsWith("Run"))
            {
                currentCircuit = new Circuit(20); 
                panel.AddCircuit(currentCircuit);
            }
            else if (line.StartsWith("-"))
            {
                var parts = line.Split(',');
                if (parts.Length < 3 || !parts[1].Contains(":") || !parts[2].Contains(":"))
                {
                    Console.WriteLine($"Warning: Skipping malformed line: {line}");
                    continue;
                }

                try
                {
                    string type = parts[0].Trim().Substring(2); 
                    string box = parts[1].Split(':')[1].Trim();
                    double wire = double.Parse(parts[2].Split(':')[1].Trim().Replace("ft", "").Trim());

                    ElectricalComponent component = type switch
                    {
                        "Receptacle" => new Receptacle(false, box),
                        "GFCI Receptacle" => new Receptacle(true, box),
                        "Light Switch" => new Light(0, 0, box, true),
                        "Light" => new Light(0, 0, box, false),
                        _ => null
                    };

                    if (component != null)
                    {
                        currentCircuit.AddComponent(new ElectricalRun(component, wire));
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Unknown component type '{type}' in line: {line}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line: {line}\n{ex.Message}");
                    continue;
                }
            }
        }

        return panel;
    }

    public string ToFormattedString()
    {
        string output = "";
        int runNumber = 1;

        foreach (var circuit in Circuits)
        {
            if (circuit == null) continue;

            output += $"Run {runNumber++}:\n";
            foreach (var item in circuit.Components)
            {
                output += $"- {item.Component.Name}, Box: {item.Component.BoxSize}, Wire: {item.WireLength} ft\n";
            }
        }

        return output;
    }
}
