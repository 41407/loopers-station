using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public interface ISpaceWorker
{
    void Enter(ILocation location);
    void Exit();
}

public class SpaceWorker : MonoBehaviour, ISpaceWorker
{
    [Inject] private ISpaceMap SpaceMap { get; }
    [Inject] private ISalary Salary { get; }
    [Inject] private ITargetMarker TargetMarker { get; }
    [Inject] private ITrader Trader { get; }
    [Inject] private List<ISpecialDelivery> SpecialDeliveries { get; }

    private ISpecialDelivery CurrentDelivery { get; set; } = new SpecialDelivery("");

    private void Start()
    {
        SetTargetLocation();
    }

    public void Enter(ILocation location)
    {
        if (location == CurrentDelivery.Destination)
        {
            Salary.Pay(CurrentDelivery);
            SetTargetLocation(location);
        }

        Trader.Enter(location);
    }

    public void Exit()
    {
        Trader.Exit();
    }

    private void SetTargetLocation(ILocation location = null)
    {
        var targetLocation = location == null ? SpaceMap.GetAnyLocation() : SpaceMap.GetAnyLocationExcept(location);

        CurrentDelivery.Name = SpecialDeliveries.OrderBy(Randomized).First().Name;
        CurrentDelivery.Source = location;
        CurrentDelivery.Destination = targetLocation;
        TargetMarker.GoTo(CurrentDelivery.Destination);
    }

    private static int Randomized(ISpecialDelivery arg) => Random.Range(-1, 1);
}
