using System;
using System.Collections.Generic;
using System.IO;

public class BillOfMaterials
{
    public int TotalNormalReceptacles { get; set; }
    public int TotalGFCIReceptacles { get; set; }
    public int TotalLightSwitches { get; set; }
    public int TotalLights { get; set; }
    public int TotalBreakers { get; set; }
    public double TotalWireLength { get; set; }
    public int TotalStraps { get; set; }

    public Dictionary<string, int> BoxCounts { get; set; } = new Dictionary<string, int>();

    public void IncrementBoxCount(string size)
    {
        if (BoxCounts.ContainsKey(size))
            BoxCounts[size]++;
        else
            BoxCounts[size] = 1;
    }

    public void Clear()
    {
        TotalNormalReceptacles = 0;
        TotalGFCIReceptacles = 0;
        TotalLightSwitches = 0;
        TotalLights = 0;
        TotalBreakers = 0;
        TotalWireLength = 0;
        TotalStraps = 0;
        BoxCounts.Clear();
    }

    public string ToFormattedString()
    {
        string output = "";
        output += $"Total Normal Receptacles: {TotalNormalReceptacles}\n";
        output += $"Total GFCI Receptacles:   {TotalGFCIReceptacles}\n";
        output += $"Total Light Switches:     {TotalLightSwitches}\n";
        output += $"Total Lights:             {TotalLights}\n";
        output += $"Total Breakers:           {TotalBreakers}\n";
        output += $"Total Wire Length:        {TotalWireLength} ft\n";
        output += $"Total Cable Straps:       {TotalStraps}\n";

        foreach (var kvp in BoxCounts)
            output += $"Total {kvp.Key} Boxes: {kvp.Value}\n";

        return output;
    }

    public void LoadFromFile(string filename)
    {
        Clear();
        if (!File.Exists(filename)) return;

        var lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            if (line.StartsWith("Total Normal Receptacles:"))
                TotalNormalReceptacles = int.Parse(line.Split(':')[1].Trim());
            else if (line.StartsWith("Total GFCI Receptacles:"))
                TotalGFCIReceptacles = int.Parse(line.Split(':')[1].Trim());
            else if (line.StartsWith("Total Light Switches:"))
                TotalLightSwitches = int.Parse(line.Split(':')[1].Trim());
            else if (line.StartsWith("Total Lights:"))
                TotalLights = int.Parse(line.Split(':')[1].Trim());
            else if (line.StartsWith("Total Breakers:"))
                TotalBreakers = int.Parse(line.Split(':')[1].Trim());
            else if (line.StartsWith("Total Wire Length:"))
                TotalWireLength = double.Parse(line.Split(':')[1].Trim().Replace("ft", "").Trim());
            else if (line.StartsWith("Total Cable Straps:"))
                TotalStraps = int.Parse(line.Split(':')[1].Trim());
            else if (line.StartsWith("Total") && line.Contains("Boxes"))
            {
                string[] parts = line.Split(new[] { "Total ", " Boxes:" }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                    BoxCounts[parts[0].Trim()] = int.Parse(parts[1].Trim());
            }
        }
    }
}
