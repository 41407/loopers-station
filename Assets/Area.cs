using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IArea
{
    List<ILocation> Locations { get; }
    IMarket Market { get; }
    Vector2 Position { get; }
    Vector2 Extents { get; }
}

public class Area : MonoBehaviour, IArea
{
    [Inject] public List<ILocation> Locations { get; }
    [Inject] public IMarket Market { get; }
    public Vector2 Position => transform.position;
    public Vector2 Extents => GetComponent<BoxCollider2D>().bounds.extents;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Market.Initialize(Locations);
        var pilot = other.attachedRigidbody.GetComponentInChildren<ISpacePilot>();
        pilot?.Enter(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var pilot = other.attachedRigidbody.GetComponentInChildren<ISpacePilot>();
        if (pilot.CurrentArea?.Equals(this) ?? true)
        {
            var warpable = GetWarpable(other);
            warpable?.WarpAround(this);
        }
    }

    private static IWarpable GetWarpable(Collider2D other) => other.attachedRigidbody.GetComponentInChildren<IWarpable>();
}
