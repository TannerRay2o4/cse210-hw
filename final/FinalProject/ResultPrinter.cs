using System;

public static class ResultPrinter
{
    public static void DisplayRuns(PanelBoard panel)
    {
        Console.Clear();
        Console.WriteLine("\n===== PANEL SUMMARY =====\n");

        int circuitNum = 1;
        foreach (var circuit in panel.Circuits)
        {
            Console.WriteLine($"Circuit #{circuitNum++} - {circuit.Components.Count} items");

            int totalReceptacles = 0;
            int gfciCount = 0;
            int boxCount = circuit.Components.Count;
            double totalWire = 0;

            foreach (var run in circuit.Components)
            {
                var component = run.Component;

                if (component is Receptacle receptacle)
                {
                    totalReceptacles++;
                    if (receptacle.IsGFCI)
                        gfciCount++;
                }

                totalWire += run.GetTotalLength();
            }

            // Add extra wire: +1 ft per box, +3 ft per breaker (1 breaker per circuit)
            totalWire += boxCount * 1; // 1 ft per box
            totalWire += 3;            // 3 ft for breaker

            int regularCount = totalReceptacles - gfciCount;

            Console.WriteLine($"  Regular Receptacles: {regularCount}");
            Console.WriteLine($"  GFCI Receptacles:    {gfciCount}");
            Console.WriteLine($"  Total Wire:          {totalWire} ft");
            Console.WriteLine($"  NEC Compliant:       {(NecComplianceChecker.IsCompliant(circuit) ? "Yes" : "No")}");
            Console.WriteLine();
        }

        Console.WriteLine("Press Enter to return to main menu...");
        Console.ReadLine();
    }

    public static void DisplayBillOfMaterials(BillOfMaterials bom)
    {
        Console.Clear();
        Console.WriteLine("\n===== BILL OF MATERIALS =====\n");

        int labelWidth = 24;  // Consistent padding for value alignment

        Console.WriteLine($"  {"Regular receptacles:".PadRight(labelWidth)} {bom.RegularReceptacles}");
        Console.WriteLine($"  {"GFCI receptacles:".PadRight(labelWidth)} {bom.GFCIReceptacles}");
        Console.WriteLine($"  {"Lights:".PadRight(labelWidth)} {bom.LightCount}");

        // Boxes
        if (bom.BoxesByType.Count == 0)
        {
            Console.WriteLine($"  {"Boxes:".PadRight(labelWidth)} 0");
        }
        else
        {
            Console.WriteLine($"  {"Boxes:".PadRight(labelWidth)}");
            foreach (var kvp in bom.BoxesByType)
            {
                string boxLabel = $"- {kvp.Key}:";
                Console.WriteLine($"    {boxLabel.PadRight(labelWidth - 2)} {kvp.Value}");
            }
        }

        Console.WriteLine($"  {"Breakers:".PadRight(labelWidth)} {bom.Breakers}");
        Console.WriteLine($"  {"Cable Straps:".PadRight(labelWidth)} {bom.CableStraps}");
        Console.WriteLine($"  {"12-2 MC Cable:".PadRight(labelWidth)} {bom.TotalWireFeet} ft");

        Console.WriteLine("\nPress Enter to return...");
        Console.ReadLine();
    }



}
