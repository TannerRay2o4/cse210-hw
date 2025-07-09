public class Receptacle : ElectricalComponent
{
    public bool IsGFCI { get; set; }
    public string BoxSize { get; set; }

    public Receptacle(string name, bool isGFCI, string boxSize) : base(name)
    {
        IsGFCI = isGFCI;
        BoxSize = boxSize;
    }
}