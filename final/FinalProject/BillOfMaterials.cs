using System;
using System.IO;
public class BillOfMaterials
{
    public int RegularReceptacles { get; set; }
    public int GFCIReceptacles { get; set; }
    public int LightCount { get; set; }
    public int Breakers { get; set; }
    public double RawWireFeet { get; set; }

    public Dictionary<string, int> BoxesByType { get; private set; } = new();

    public double TotalWireFeet =>
        RawWireFeet + BoxesByType.Values.Sum() * 1 + Breakers * 3;

    // 1 cable strap per 3 ft of wire, rounded up
    public int CableStraps => (int)Math.Ceiling(TotalWireFeet / 3.0);

    public void AddReceptacle(bool isGFCI)
    {
        if (isGFCI) GFCIReceptacles++;
        else RegularReceptacles++;
    }

    public void AddLight() => LightCount++;
    public void AddBreaker() => Breakers++;
    public void AddWire(double feet) => RawWireFeet += feet;

    public void AddBox(string boxType)
    {
        if (!BoxesByType.ContainsKey(boxType))
            BoxesByType[boxType] = 0;
        BoxesByType[boxType]++;
    }

    public void SaveToFile(string path)
    {
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine(RegularReceptacles);
            writer.WriteLine(GFCIReceptacles);
            writer.WriteLine(LightCount);
            writer.WriteLine(Breakers);
            writer.WriteLine(RawWireFeet);

            writer.WriteLine(BoxesByType.Count);
            foreach (var kvp in BoxesByType)
            {
                writer.WriteLine(kvp.Key);
                writer.WriteLine(kvp.Value);
            }
        }
    }

    public static BillOfMaterials LoadFromFile(string path)
    {
        BillOfMaterials bom = new BillOfMaterials();

        using (StreamReader reader = new StreamReader(path))
        {
            bom.RegularReceptacles = int.Parse(reader.ReadLine());
            bom.GFCIReceptacles = int.Parse(reader.ReadLine());
            bom.LightCount = int.Parse(reader.ReadLine());
            bom.Breakers = int.Parse(reader.ReadLine());
            bom.RawWireFeet = double.Parse(reader.ReadLine());

            int boxCount = int.Parse(reader.ReadLine());
            for (int i = 0; i < boxCount; i++)
            {
                string key = reader.ReadLine();
                int value = int.Parse(reader.ReadLine());
                bom.BoxesByType[key] = value;
            }
        }

        return bom;
    }

}

