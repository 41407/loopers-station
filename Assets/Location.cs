using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Location : MonoBehaviour, ILocation
{
    private List<ICommodity> Commodities { get; } = new List<ICommodity>();

    public Vector3 Position => transform.position;

    public void SetCommodities(IEnumerable<ICommodity> commodities)
    {
        Commodities.Clear();
        commodities.ToList().ForEach(commodity => Commodities.Add(new Commodity(commodity.Name, Random.Range(commodity.Value / 2, commodity.Value * 2))));
    }

    public int GetDealFor(ICommodity commodity) => Commodities.Find(Matching(commodity))?.Value ?? 0;

    private static Predicate<ICommodity> Matching(ICommodity commodity)
    {
        return c => c.Name == commodity.Name;
    }

    public ICommodity GetBestSale()
    {
        return Commodities.First();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.attachedRigidbody.GetComponentInChildren<ISpaceWorker>().Enter(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.attachedRigidbody.GetComponentInChildren<ISpaceWorker>().Exit();
    }
}

public interface ILocation
{
    Vector3 Position { get; }
    void SetCommodities(IEnumerable<ICommodity> commodities);
    int GetDealFor(ICommodity commodity);
    ICommodity GetBestSale();
}
