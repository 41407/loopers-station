using System.Collections.Generic;
using System.Linq;
using Zenject;

public interface IMarket
{
    void RegisterTransaction(ICommodity commodity);
    void Initialize(List<ILocation> locations);
}

public class Market : IMarket
{
    [Inject] private List<ICommodity> Commodities { get; }

    private int TransactionCount { get; set; }

    public void RegisterTransaction(ICommodity commodity)
    {
        TransactionCount++;
        if (TransactionCount > 5) RandomizeCommodities();
    }

    public void Initialize(List<ILocation> locations)
    {
        Locations = locations;
        RandomizeCommodities();
    }

    public List<ILocation> Locations { get; set; }

    private void RandomizeCommodities() => Locations.ForEach(SetCommodities);

    private void SetCommodities(ILocation location) => location.SetCommodities(Commodities.OrderBy(Random).Take(5));
    private static int Random(ICommodity commodity) => UnityEngine.Random.Range(-1, 1);
}
