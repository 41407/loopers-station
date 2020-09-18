internal interface ICargo
{
    string Description { get; }
    ICommodity Commodity { get; }
    bool IsFull { get; }
    void Unload();
    void Load(ICommodity commodity);
}

public class Cargo : ICargo
{
    public string Description => Commodity?.Name;
    public ICommodity Commodity { get; private set; }
    public bool IsFull => Commodity != null;

    public void Unload()
    {
        Commodity = null;
    }

    public void Load(ICommodity commodity)
    {
        Commodity = commodity;
    }
}
