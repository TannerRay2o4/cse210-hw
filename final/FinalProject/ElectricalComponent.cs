public abstract class ElectricalComponent
{
    public string Name { get; set; }

    public ElectricalComponent(string name)
    {
        Name = name;
    }
}