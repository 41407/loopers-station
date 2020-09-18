using System.Collections.Generic;
using System.Linq;

public interface ISpaceMap
{
    ILocation GetAnyLocation();
    ILocation GetAnyLocationExcept(ILocation location);
    List<ILocation> KnownLocations { get; }
    void Add(List<ILocation> locations);
}

public class SpaceMap : ISpaceMap
{
    public List<ILocation> KnownLocations { get; } = new List<ILocation>();

    public void Add(List<ILocation> locations) => locations.FindAll(Unknown).ForEach(KnownLocations.Add);

    private bool Unknown(ILocation location) => !KnownLocations.Contains(location);

    public ILocation GetAnyLocation() => PickAnyFrom(KnownLocations);

    public ILocation GetAnyLocationExcept(ILocation location) => PickAnyFrom(AllExcept(location));

    private IEnumerable<ILocation> AllExcept(ILocation location) => KnownLocations.FindAll(loc => loc != location);

    private static ILocation PickAnyFrom(IEnumerable<ILocation> locations) => locations.OrderBy(Random).First();

    private static int Random(ILocation location) => UnityEngine.Random.Range(-1, 1);
}
