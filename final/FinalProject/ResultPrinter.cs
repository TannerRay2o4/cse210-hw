using System;

public static class ResultPrinter
{
    public static void DisplayBillOfMaterials(BillOfMaterials bom)
    {
        Console.Clear();
        Console.WriteLine("=== Bill of Materials Summary ===\n");

        Console.WriteLine($"Total Normal Receptacles: {bom.TotalNormalReceptacles}");
        Console.WriteLine($"Total GFCI Receptacles:   {bom.TotalGFCIReceptacles}");
        Console.WriteLine($"Total Light Switches:     {bom.TotalLightSwitches}");
        Console.WriteLine($"Total Lights:             {bom.TotalLights}");
        Console.WriteLine($"Total Breakers:           {bom.TotalBreakers}");
        Console.WriteLine($"Total Wire Straps:        {bom.TotalStraps}");
        Console.WriteLine($"Total Wire Length:        {bom.TotalWireLength} ft\n");

        Console.WriteLine("Box Count Details:");
        foreach (var entry in bom.BoxCounts)
        {
            Console.WriteLine($"  {entry.Key}: {entry.Value}");
        }

        Console.WriteLine("\nPress Enter to return...");
        Console.ReadLine();
    }

    public static void DisplayRuns(PanelBoard panel, BillOfMaterials bom)
    {
        Console.Clear();
        Console.WriteLine("=== Panel Summary ===\n");

        for (int i = 0; i < panel.Circuits.Count; i++)
        {
            var circuit = panel.Circuits[i];
            if (circuit == null) continue;

            Console.WriteLine($"Circuit #{i + 1}: \nBreaker Size = {circuit.BreakerSize}A");

            int normalReceptacles = 0;
            int gfciReceptacles = 0;
            int lightSwitches = 0;
            int lights = 0;
            int totalStraps = 0;
            double totalWire = 0;

            foreach (var run in circuit.Components)
            {
                totalWire += run.WireLength;
                totalStraps += (int)Math.Ceiling(run.WireLength / 3.0);

                if (run.Component is Receptacle r)
                {
                    if (r.IsGFCI)
                        gfciReceptacles++;
                    else
                        normalReceptacles++;
                }
                else if (run.Component is Light l)
                {
                    if (l.IsSwitch)
                        lightSwitches++;
                    else
                        lights++;
                }
            }

            Console.WriteLine($"Total Normal Receptacles: {normalReceptacles}");
            Console.WriteLine($"Total GFCI Receptacles:   {gfciReceptacles}");
            Console.WriteLine($"Total Light Switches:     {lightSwitches}");
            Console.WriteLine($"Total Lights:             {lights}");
            Console.WriteLine($"Total Breakers:           1");
            Console.WriteLine($"Total Wire Straps:        {totalStraps}");
            Console.WriteLine($"Total Wire Length:        {totalWire} ft\n");
        }

        Console.WriteLine("Press Enter to return...");
        Console.ReadLine();
    }
}
