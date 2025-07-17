using System;
using System.IO;

public class LayoutPlanner
{
    private PanelBoard panel;
    private BillOfMaterials bom;

    public LayoutPlanner(PanelBoard panel, BillOfMaterials bom)
    {
        this.panel = panel;
        this.bom = bom;
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Electrical Layout Planner ===");
            Console.WriteLine("1. Add New Run");
            Console.WriteLine("2. Edit Existing Run");
            Console.WriteLine("3. Display Runs");
            Console.WriteLine("4. Display Bill of Materials (BOM)");
            Console.WriteLine("5. Load BOM from file");
            Console.WriteLine("6. Save BOM to file");
            Console.WriteLine("7. Exit");
            Console.Write("\nEnter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    AddNewRun();
                    break;
                case "2":
                    Console.Clear();
                    EditExistingRun();
                    break;
                case "3":
                    Console.Clear();
                    RebuildBOMFromPanel();
                    ResultPrinter.DisplayRuns(panel, bom);
                    break;
                case "4":
                    Console.Clear();
                    ResultPrinter.DisplayBillOfMaterials(bom);
                    break;
                case "5":
                    Console.Clear();
                    if (!File.Exists("BOM.txt"))
                    {
                        Console.WriteLine("No BOM.txt file found.");
                    }
                    else
                    {
                        bom.LoadFromFile("BOM.txt");
                        panel = PanelBoard.LoadFromFile("BOM.txt");
                        RebuildBOMFromPanel(); 
                        Console.WriteLine("BOM and Panel loaded successfully.");
                    }
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                    break;
                case "6":
                    Console.Clear();
                    File.WriteAllText("BOM.txt", bom.ToFormattedString() + "\n" + panel.ToFormattedString());
                    Console.WriteLine("BOM and Panel saved to BOM.txt. \n\nPress Enter to continue...");
                    Console.ReadLine();
                    break;
                case "7":
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice. Press Enter to try again...");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private void EditExistingRun()
    {
        if (panel?.Circuits == null || panel.Circuits.Count == 0 || panel.Circuits.TrueForAll(c => c == null))
        {
            Console.WriteLine("No Runs have been established.\n\nPress Enter to continue...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("1. Delete Existing Run\n2. Return to Main Menu");
        int option = UserInputHandler.GetInt("Enter your choice: ", 1, 2);

        if (option == 1)
        {
            Console.Clear();
            Console.WriteLine("=== Existing Runs ===\n");

            for (int i = 0; i < panel.Circuits.Count; i++)
            {
                var circuit = panel.Circuits[i];
                if (circuit == null) continue;

                Console.WriteLine($"Circuit #{i + 1}: \nBreaker Size:             {circuit.BreakerSize}A");

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

            int selected = UserInputHandler.GetInt("Enter run number to delete: ", 1, panel.Circuits.Count);
            if (panel.Circuits[selected - 1] != null)
            {
                panel.Circuits[selected - 1] = null;
                Console.Clear();
                Console.WriteLine($"\nCircuit #{selected} deleted.");

                Console.WriteLine("1. Add New Run to Replace Deleted\n2. Return to Main Menu");
                int nextChoice = UserInputHandler.GetInt("Enter your choice: ", 1, 2);
                if (nextChoice == 1)
                {
                    Console.Clear();
                    AddNewRun(selected - 1);
                }

                RebuildBOMFromPanel();
            }
            else
            {
                Console.WriteLine("That run is already empty.");
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }

    public void RebuildBOMFromPanel()
    {
        bom.Clear();

        foreach (var circuit in panel.Circuits)
        {
            if (circuit == null) continue;

            double totalWire = 0;

            for (int j = 0; j < circuit.Components.Count; j++)
            {
                var run = circuit.Components[j];
                totalWire += run.WireLength;

                if (run.Component is Receptacle r)
                {
                    if (r.IsGFCI)
                        bom.TotalGFCIReceptacles++;
                    else
                        bom.TotalNormalReceptacles++;
                }
                else if (run.Component is Light l)
                {
                    if (l.IsSwitch)
                        bom.TotalLightSwitches++;
                    else
                        bom.TotalLights++;
                }

                bom.IncrementBoxCount(run.Component.BoxSize);
            }

            bom.TotalBreakers++;
            bom.TotalWireLength += totalWire;
            bom.TotalStraps += (int)Math.Ceiling(totalWire / 3.0);
        }
    }

    private void AddNewRun(int? slotIndex = null)
    {
        Console.WriteLine("Select run type:\n1. Receptacles\n2. Lights");
        int runChoice = UserInputHandler.GetInt("Enter choice (1 or 2): ", 1, 2);
        string runType = runChoice == 1 ? "receptacle" : "light";

        int numItems = UserInputHandler.GetInt($"How many {(runType == "receptacle" ? "receptacles" : "lights")} on this run? ", 1, 100);
        int gfciCount = 0;

        if (runType == "receptacle")
            gfciCount = UserInputHandler.GetInt("How many are GFCI Receptacles? ", 0, numItems);

        int breakerSize = UserInputHandler.GetInt("Breaker size (amps): ", 15, 60);

        Circuit circuit = new Circuit(breakerSize);
        double totalWireForCircuit = 0;

        if (runType == "receptacle")
        {

            for (int i = 0; i < numItems; i++)
            {
                double height = UserInputHandler.GetDouble($"\nReceptacle #{i + 1}: \n    Height off ground (ft): ", 0);
                double length = (i == 0)
                    ? UserInputHandler.GetDouble("    Horizontal distance to panel (ft): ", 0)
                    : UserInputHandler.GetDouble($"    Horizontal distance from Receptacle #{i} (ft): ", 0);

                Console.WriteLine("    Box size:\n\n    1. 4x4 Deep Box\n    2. Other");
                int boxChoice = UserInputHandler.GetInt("    Enter choice: ", 1, 2);
                string boxSize = boxChoice == 1 ? "4x4 Deep" : "Other";

                bool isGFCI = i < gfciCount;
                var component = new Receptacle(isGFCI, boxSize);
                double vertical = (i < numItems - 1) ? height * 2 : height;
                double totalWire = vertical + length + 1;

                circuit.AddComponent(new ElectricalRun(component, totalWire));
                totalWireForCircuit += totalWire;
            }
        }
        else
        {
            double switchHeight = UserInputHandler.GetDouble("\nLight Switch:\n    Vertical distance from ceiling (ft): ", 0);
            double switchLength = UserInputHandler.GetDouble("    Horizontal distance to panel (ft): ", 0);
            Console.WriteLine("    Switch box size:\n    1. 4x4 Deep Box\n    2. Other");
            int switchBoxChoice = UserInputHandler.GetInt("    Enter choice: ", 1, 2);
            string switchBoxSize = switchBoxChoice == 1 ? "4x4 Deep" : "Other";

            var switchComponent = new Light(0, 0, switchBoxSize, true);
            double switchWire = (switchHeight * 2) + switchLength + 1;
            circuit.AddComponent(new ElectricalRun(switchComponent, switchWire));
            totalWireForCircuit += switchWire;

            for (int i = 0; i < numItems; i++)
            {
                double lightLength = (i == 0)
                    ? UserInputHandler.GetDouble($"\nLight #{i + 1}: \n    Horizontal distance from switch (ft): ", 0)
                    : UserInputHandler.GetDouble($"Light #{i + 1}: \n    Horizontal distance from Light {i} (ft): ", 0);

                Console.WriteLine("    Box size:\n    1. 4 in Octagon box\n    2. Other");
                int boxChoice = UserInputHandler.GetInt("    Enter choice: ", 1, 2);
                string boxSize = boxChoice == 1 ? "4 in Octagon" : "Other";

                var lightComponent = new Light(0, 0, boxSize, false);
                double lightWire = lightLength + 1;
                circuit.AddComponent(new ElectricalRun(lightComponent, lightWire));
                totalWireForCircuit += lightWire;
            }
        }

        totalWireForCircuit += 3; // breaker slack
        if (slotIndex.HasValue && slotIndex.Value < panel.Circuits.Count)
            panel.Circuits[slotIndex.Value] = circuit;
        else
            panel.AddCircuit(circuit);

    }
}
