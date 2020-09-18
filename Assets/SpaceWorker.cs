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
    [Inject] private ISpecialDelivery CurrentDelivery { get; set; }

    private void Start()
    {
        UnloadSpecialCargo();
    }


    public void Enter(ILocation location)
    {
        if (location == CurrentDelivery.Destination)
        {
            if (CurrentDelivery.Destination == CurrentDelivery.Source)
            {
                LoadSpecialCargoFrom(location);
            }
            else
            {
                Salary.Pay(CurrentDelivery);
                UnloadSpecialCargoTo(location);
            }
        }

        Trader.Enter(location);
    }

    private void UnloadSpecialCargo()
    {
        UnloadSpecialCargoTo(null);
    }

    private void UnloadSpecialCargoTo(ILocation location)
    {
        var targetLocation = location == null ? SpaceMap.GetAnyLocation() : SpaceMap.GetAnyLocationExcept(location);

        CurrentDelivery.Name = "";
        CurrentDelivery.Source = targetLocation;
        CurrentDelivery.Destination = targetLocation;
        TargetMarker.GoTo(CurrentDelivery.Destination);
    }

    public void Exit()
    {
        Trader.Exit();
    }

    private void LoadSpecialCargoFrom(ILocation location = null)
    {
        var targetLocation = location == null ? SpaceMap.GetAnyLocation() : SpaceMap.GetAnyLocationExcept(location);

        CurrentDelivery.Name = SpecialDeliveries.OrderBy(Randomized).First().Name;

        CurrentDelivery.Source = location;
        CurrentDelivery.Destination = targetLocation;
        TargetMarker.GoTo(CurrentDelivery.Destination);
    }

    private static int Randomized(ISpecialDelivery arg) => Random.Range(-1, 1);
}
