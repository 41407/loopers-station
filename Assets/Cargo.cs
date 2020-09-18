internal interface ICargo
{
    string Description { get; }
    ICommodity Commodity { get; }
    bool IsFull { get; }
    void UnloadCommodity();
    void Load(ICommodity commodity);
}

public class Cargo : ICargo
{
    public string Description => Commodity?.Name;
    public ICommodity Commodity { get; private set; }
    public ISpecialDelivery SpecialDelivery{ get; private set; }

    public bool IsFull => Commodity != null;

    public void UnloadCommodity()
    {
        Commodity = null;
    }

    public void Load(ICommodity commodity)
    {
        Commodity = commodity;
    }
    public void Load(ISpecialDelivery specialDelivery)
    {
        SpecialDelivery = specialDelivery;
    }

    public void UnloadSpecialDelivery()
    {
        SpecialDelivery = null;
    }
}
