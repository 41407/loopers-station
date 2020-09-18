using System.Collections.Generic;
using System.Linq;
using Zenject;

public interface IMarket
{
    void RegisterTransaction(ICommodity commodity);
}

public class Market : IMarket
{
    private ISpaceMap Map { get; }
    private List<ICommodity> Commodities { get; }

    private int TransactionCount { get; set; }

    [Inject]
    public Market(ISpaceMap map, List<ICommodity> commodities)
    {
        Map = map;
        Commodities = commodities;
        RandomizeCommodities();
    }

    public void RegisterTransaction(ICommodity commodity)
    {
        TransactionCount++;
        if (TransactionCount > 5) RandomizeCommodities();
    }

    private void RandomizeCommodities() => Map.Locations.ForEach(SetCommodities);

    private void SetCommodities(ILocation location) => location.SetCommodities(Commodities.OrderBy(Random).Take(5));
    private static int Random(ICommodity commodity) => UnityEngine.Random.Range(-1, 1);
}
