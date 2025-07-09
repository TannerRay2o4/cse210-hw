using System;

class Program
{
    static void Main(string[] args)
    {
        PanelBoard panel = new PanelBoard();
        BillOfMaterials bom = new BillOfMaterials();
        LayoutPlanner planner = new LayoutPlanner(panel, bom);
        planner.Run();
    }
}