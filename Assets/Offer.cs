public interface IOffer
{
    int Value { get; set; }
    ICommodity Commodity { get; set; }
    bool IsAPurchase { get; }
    bool IsASale { get; }
    bool IsAvailable { get; }
    string Description { get; set; }
    void Clear();
}

internal class Offer : IOffer
{
    public int Value { get; set; }
    public ICommodity Commodity { get; set; }
    public bool IsAPurchase => Value < 0;
    public bool IsASale => Value > 0;
    public bool IsAvailable => IsAPurchase || IsASale;
    public string Description { get; set; }

    public void Clear()
    {
        Value = 0;
        Commodity = null;
    }
}
