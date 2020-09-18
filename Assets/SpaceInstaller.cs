using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SpaceInstaller : MonoInstaller
{
    [SerializeField] private GameObject targetMarkerPrefab;

    public override void InstallBindings()
    {
        Container.Bind<ISteerable>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IKeybindings>().To<DefaultKeybindings>().AsSingle();
        Container.Bind<ISpaceMap>().To<SpaceMap>().AsSingle();
        Container.Bind<ILocation>().FromComponentsInChildren().AsTransient();
        Container.Bind<IArea>().FromComponentInParents().AsTransient();
        Container.Bind<ISalary>().To<SpaceWorkerSalary>().AsSingle();
        Container.Bind<IBankAccount>().To<BankAccount>().AsSingle();
        Container.Bind<Text>().FromComponentSibling().AsTransient();
        Container.Bind<ITargetMarker>().FromComponentInNewPrefab(targetMarkerPrefab).AsSingle();
        Container.Bind<ITrader>().To<Trader>().AsSingle();
        Container.Bind<IMarket>().To<Market>().AsTransient();
        Container.Bind<ICommodity>().FromMethodMultiple(CreateCommodities).AsSingle();
        Container.Bind<List<ISpecialDelivery>>().FromInstance(CreateSpecialDeliveries()).AsSingle();
        Container.Bind<ISpecialDelivery>().To<SpecialDelivery>().AsSingle();
        Container.Bind<ICargo>().To<Cargo>().AsSingle();
    }

    private static IEnumerable<ICommodity> CreateCommodities(InjectContext arg)
    {
        var names = new List<string> {"Mud", "Rocks", "Bronze Ore", "Iron Ore", "Steel Ore", "Mithril Ore", "Adamantium Ore", "Eternium Ore", "Quantum Ore", "God Ore"};
        var values = new List<int> {10, 50, 100, 200, 400, 800, 1600, 3200, 6400, 12800};
        var commodities = MapIntoCommodities(names, values);
        return commodities;
    }

    private static List<ISpecialDelivery> CreateSpecialDeliveries()
    {
        var names = new List<string> {"Suitcase full of important Documents", "A very small crate", "A very hard box", "Loaf of Elven Bread", "Some weird musical instrument", "A sleeping cat", "A stack of papers", "Empty bottles", "A sealed letter", "A paper you've been told not to fold", "A sack of potatoes"};
        return MapIntoSpecialDeliveries(names);
    }

    private static List<ISpecialDelivery> MapIntoSpecialDeliveries(IEnumerable<string> names) => names.Select(CreateSpecialDelivery).ToList();

    private static ISpecialDelivery CreateSpecialDelivery(string name) => new SpecialDelivery(name);

    private static IEnumerable<ICommodity> MapIntoCommodities(List<string> names, List<int> values)
    {
        var list = new List<ICommodity>();
        for (var i = 0; i < WhicheverCountIsSmaller(names, values); i++)
        {
            var name = names[i];
            var value = values[i];
            var commodity = CreateCommodity(name, value);
            list.Add(commodity);
        }

        return list;
    }

    private static int WhicheverCountIsSmaller(IList names, IList values)
    {
        var x = new[] {names, values};

        return x.OrderBy(list => list.Count).First().Count;
    }

    private static Commodity CreateCommodity(string name, int value) => new Commodity(name, value);
}
