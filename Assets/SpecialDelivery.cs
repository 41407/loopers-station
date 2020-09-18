public interface ISpecialDelivery
{
    string Name { get; set; }
    ILocation Source { get; set; }
    ILocation Destination { get; set; }
}

internal class SpecialDelivery : ISpecialDelivery
{
    public string Name { get; set; }
    public ILocation Source { get; set; }
    public ILocation Destination { get; set; }

    public SpecialDelivery(string name)
    {
        Name = name;
    }
}
