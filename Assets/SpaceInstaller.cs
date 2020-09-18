using System.Collections.Generic;
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
        Container.Bind<ILocation>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<ISalary>().To<SpaceWorkerSalary>().AsSingle();
        Container.Bind<IBankAccount>().To<BankAccount>().AsSingle();
        Container.Bind<Text>().FromComponentSibling().AsTransient();
        Container.Bind<ITargetMarker>().FromComponentInNewPrefab(targetMarkerPrefab).AsSingle();
        Container.Bind<ITrader>().To<Trader>().AsSingle();
        Container.Bind<IMarket>().To<Market>().AsSingle();
        Container.Bind<ICommodity>().FromMethodMultiple(CreateCommodities).AsSingle();
        Container.Bind<ICargo>().To<Cargo>().AsSingle();
    }

    private static IEnumerable<ICommodity> CreateCommodities(InjectContext arg)
    {
        var names = new List<string> {"Mud", "Rocks", "Bronze Ore", "Iron Ore", "Steel Ore", "Mithril Ore", "Adamantium Ore", "Eternium Ore", "Quantum Ore", "God Ore"};
        var values = new List<int> {10, 50, 100, 200, 400, 800, 1600, 3200, 6400, 12800};
        var commodities = MapIntoCommodities(names, values);
        return commodities;
    }

    private static IEnumerable<ICommodity> MapIntoCommodities(List<string> names, List<int> values)
    {
        var list = new List<ICommodity>();
        for (var i = 0; i < WhicheverHasSmallerCount(names, values); i++)
        {
            var name = names[i];
            var value = values[i];
            var commodity = CreateCommodity(name, value);
            list.Add(commodity);
        }
        return list;
    }

    private static int WhicheverHasSmallerCount(List<string> names, List<int> values)
    {
        return Mathf.Min(names.Count, values.Count);
    }

    private static Commodity CreateCommodity(string name, int value) => new Commodity(name, value);
}
