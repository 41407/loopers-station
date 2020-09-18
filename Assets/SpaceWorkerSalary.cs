using UnityEngine;
using Zenject;

internal interface ISalary
{
    void Pay(ILocation source, ILocation destination);
    void Pay(ISpecialDelivery currentDelivery);
}

public class SpaceWorkerSalary : ISalary
{
    [Inject] private IBankAccount account;

    public void Pay(ILocation source, ILocation destination)
    {
        account.Credit(DistanceBetween(source, destination));
    }

    public void Pay(ISpecialDelivery currentDelivery)
    {
        var source = currentDelivery.Source;
        var destination = currentDelivery.Destination;
        Pay(source, destination);
    }

    private static int DistanceBetween(ILocation source, ILocation destination)
    {
        if (source == null || destination == null) return 0;
        return Mathf.RoundToInt(Vector3.Distance(source.Position, destination.Position));
    }
}
