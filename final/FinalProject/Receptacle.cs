public class Receptacle : ElectricalComponent
{
    public bool IsGFCI { get; }

    public Receptacle(bool isGFCI, string boxSize)
        : base(isGFCI ? "GFCI Receptacle" : "Receptacle", boxSize)
    {
        IsGFCI = isGFCI;
    }

    public override string BoxSize { get; set; }

    public override double WireLengthFromPrevious()
    {
        return 0; // Adjust if you plan to calculate wire for receptacles
    }
}
