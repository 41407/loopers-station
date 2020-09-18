using UnityEngine;
using Zenject;

internal interface ISalary
{
    void Pay(ILocation source, ILocation target);
}

public class SpaceWorkerSalary : ISalary
{
    [Inject] private IBankAccount account;

    public void Pay(ILocation source, ILocation target)
    {
        account.Credit(DistanceBetween(source, target));
    }

    private static int DistanceBetween(ILocation source, ILocation target)
    {
        return Mathf.RoundToInt(Vector3.Distance(source.Position, target.Position));
    }
}
