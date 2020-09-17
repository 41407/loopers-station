using System.Collections.Generic;
using System.Linq;
using Zenject;

internal interface ISpaceMap
{
    ILocation GetAnyLocation();
    ILocation GetAnyLocationExcept(ILocation location);
}

public class SpaceMap : ISpaceMap
{
    [Inject] private List<ILocation> Locations { get; }

    public ILocation GetAnyLocation() => PickAnyFrom(Locations);

    public ILocation GetAnyLocationExcept(ILocation location) => PickAnyFrom(AllExcept(location));

    private IEnumerable<ILocation> AllExcept(ILocation location) => Locations.FindAll(loc => loc != location);

    private static ILocation PickAnyFrom(IEnumerable<ILocation> locations) => locations.OrderBy(Random).First();

    private static int Random(ILocation location) => UnityEngine.Random.Range(-1, 1);
}
