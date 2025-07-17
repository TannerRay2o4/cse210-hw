public class Light : ElectricalComponent
{
    public bool IsSwitch { get; set; } = false;

    public Light(double height, double horizontalDistance, string boxSize, bool isSwitch = false)
        : base(isSwitch ? "Light Switch" : "Light", boxSize)
    {
        Height = height;
        HorizontalDistance = horizontalDistance;
        BoxSize = boxSize;
        IsSwitch = isSwitch;
    }

    public double Height { get; set; }
    public double HorizontalDistance { get; set; }

    public override string BoxSize { get; set; }

    public override double WireLengthFromPrevious()
    {
        return 2 * Height + HorizontalDistance + 1;
    }
}
