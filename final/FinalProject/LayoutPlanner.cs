using System;

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
                    AddNewRun();
                    break;
                case "2":
                    Console.WriteLine("No Runs have been established.");
                    Console.ReadLine();
                    break;
                case "3":
                    ResultPrinter.DisplayRuns(panel);
                    break;
                case "4":
                    ResultPrinter.DisplayBillOfMaterials(bom);
                    break;
                case "5":
                    if (!File.Exists("BOM.txt") || new FileInfo("BOM.txt").Length == 0)
                    {
                        Console.WriteLine("BOM file is empty. Nothing to load.");
                    }
                    else
                    {
                        bom = BillOfMaterials.LoadFromFile("BOM.txt");
                        Console.WriteLine("BOM loaded successfully.");
                    }
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    break;
                case "6":
                    bom.SaveToFile("BOM.txt");
                    Console.WriteLine("BOM saved. Press Enter to continue...");
                    Console.ReadLine();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Press Enter to try again...");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private void AddNewRun()
    {
        Console.Clear();
        Console.WriteLine("Select run type:\n1. Receptacles\n2. Lights");
        int runChoice = UserInputHandler.GetInt("Enter choice (1 or 2): ", 1, 2);
        string runType = runChoice == 1 ? "receptacle" : "light";

        int numItems = UserInputHandler.GetInt($"How many {(runType == "receptacle" ? "receptacles" : "lights")} are on this run? ", 1, 100);

        int gfciCount = 0;
        if (runType == "receptacle")
        {
            gfciCount = UserInputHandler.GetInt("How many of these receptacles are GFCI? ", 0, numItems);
        }

        int breakerSize = UserInputHandler.GetInt("Breaker size (amps): ", 15, 60);

        Circuit circuit = new Circuit(breakerSize);
        double totalWireForCircuit = 0;

        if (runType == "receptacle")
        {


            for (int i = 0; i < numItems; i++)
            {
                Console.WriteLine($"\n    {runType.First().ToString().ToUpper() + runType.Substring(1)} #{i + 1}");

                string name = UserInputHandler.GetString("    Name: ");
                double height = UserInputHandler.GetDouble("    Height off ground (ft): ", 0);
                double length;

                if (i == 0)
                    length = UserInputHandler.GetDouble("    Horizontal wire run to panel (ft): ", 0);
                else
                    length = UserInputHandler.GetDouble($"    Horizontal wire run to {runType} #{i} (ft): ", 0);

                string boxSize = "";
                if (runType == "receptacle")
                {
                    Console.WriteLine("    Select box size:\n    1. 4x4 Deep Box\n    2. Other");
                    int boxChoice = UserInputHandler.GetInt("    Enter choice: ", 1, 2);
                    boxSize = boxChoice == 1 ? "4x4 Deep" : "Other";
                }

                ElectricalComponent component;
                if (runType == "receptacle")
                {
                    bool isGFCI = i < gfciCount;
                    component = new Receptacle(name, isGFCI, boxSize);
                    bom.AddReceptacle(isGFCI);
                }
                else
                {
                    component = new Light(name);
                    bom.AddLight();
                }

                double verticalLength = (i < numItems - 1) ? height * 2 : height;
                double totalWire = verticalLength + length;
                totalWireForCircuit += totalWire;

                ElectricalRun run = new ElectricalRun(component, totalWire);
                circuit.AddComponent(run);
                bom.AddBox(boxSize);
            }

            bom.AddBreaker();
            bom.AddWire(totalWireForCircuit);
            panel.AddCircuit(circuit);
        }
    }
}
