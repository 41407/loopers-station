using UnityEngine;
using Zenject;

public interface ISpaceWorker
{
    void Enter(ILocation location);
}

public class SpaceWorker : MonoBehaviour, ISpaceWorker
{
    [Inject] private ISpaceMap SpaceMap { get; }
    [Inject] private ISalary Salary { get; }
    [Inject] private ITargetMarker TargetMarker { get; }

    private ILocation SourceLocation { get; set; }
    private ILocation TargetLocation { get; set; }

    private void Start()
    {
        SetTargetLocation();
        SourceLocation = TargetLocation;
    }

    public void Enter(ILocation location)
    {
        if (location == TargetLocation)
        {
            Salary.Pay(SourceLocation, TargetLocation);
            SetTargetLocation(location);
        }
    }

    private void SetTargetLocation(ILocation location = null)
    {
        if (location == null)
        {
            TargetLocation = SpaceMap.GetAnyLocation();
        }
        else
        {
            TargetLocation = SpaceMap.GetAnyLocationExcept(location);
        }

        TargetMarker.GoTo(TargetLocation);
    }
}
