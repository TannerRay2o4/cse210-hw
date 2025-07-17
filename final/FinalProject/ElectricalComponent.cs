public abstract class ElectricalComponent
{
    public string Name { get; set; }

    public ElectricalComponent(string name, string boxSize)
    {
        Name = name;
        BoxSize = boxSize;
    }

    // Abstract property for box size (must be implemented by derived classes)
    public abstract string BoxSize { get; set; }

    // Abstract method for wire length calculation from previous component
    public abstract double WireLengthFromPrevious();
}
