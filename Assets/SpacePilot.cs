using UnityEngine;
using Zenject;

internal interface ISpacePilot
{
    IArea CurrentArea { get; }
    void Enter(IArea area);
}

internal class SpacePilot : MonoBehaviour, ISpacePilot
{
    [Inject] private ISpaceMap SpaceMap { get; }
    [Inject] private ISpaceCamera SpaceCamera { get; }
    [Inject] private ITrader Trader { get; }
    [Inject] private ISpaceWorker Worker { get; }

    public IArea CurrentArea { get; private set; }

    public void Enter(IArea area)
    {
        CurrentArea = area;
        SpaceMap.Add(area.Locations);
        Trader.Enter(area);
        Worker.Enter(area);
        SpaceCamera.MoveTo(area);
    }
}
