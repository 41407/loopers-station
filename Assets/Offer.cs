public interface IOffer
{
    int Value { get; set; }
    string Description { get; set; }
}

internal class Offer : IOffer
{
    public int Value { get; set; }
    public string Description { get; set; }
}
