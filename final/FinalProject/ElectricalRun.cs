public class ElectricalRun
{
    public ElectricalComponent Component { get; }
    public double WireLength { get; }

    public ElectricalRun(ElectricalComponent component, double wireLength)
    {
        Component = component;
        WireLength = wireLength;
    }

    public double GetTotalLength()
    {
        return WireLength;
    }
}