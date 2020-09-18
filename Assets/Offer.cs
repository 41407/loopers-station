public interface IOffer
{
    int Value { get; set; }
    string Description { get; set; }
    ICommodity Commodity { get; set; }
    bool IsAPurchase { get; }
    bool IsASale { get; }
    bool IsGiven { get; }
    void Clear();
}

internal class Offer : IOffer
{
    public int Value { get; set; }
    public string Description { get; set; }
    public ICommodity Commodity { get; set; }
    public bool IsAPurchase => Value < 0;
    public bool IsASale => Value > 0;
    public bool IsGiven => IsAPurchase || IsASale;

    public void Clear()
    {
        Value = 0;
        Description = "";
        Commodity = null;
    }
}
