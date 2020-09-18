using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Location : MonoBehaviour, ILocation
{
    private List<ICommodity> Stockpiles { get; } = new List<ICommodity>();

    public Vector3 Position => transform.position;

    public void SetCommodities(IEnumerable<ICommodity> commodities)
    {
        Stockpiles.Clear();
        commodities.ToList().ForEach(commodity => Stockpiles.Add(new Commodity(commodity.Name, Random.Range(commodity.Value / 2, commodity.Value + commodity.Value / 2))));
    }

    public int GetImportOfferFor(ICommodity commodity) => FindValueOf(commodity);

    private int FindValueOf(ICommodity commodity) => Find(commodity)?.Value ?? 0;

    private ICommodity Find(ICommodity commodity) => Stockpiles.Find(Matching(commodity));

    private static Predicate<ICommodity> Matching(ICommodity commodity) => c => c.Name == commodity.Name;

    public ICommodity GetBestSale() => Stockpiles.First();

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
    int GetImportOfferFor(ICommodity commodity);
    ICommodity GetBestSale();
}
