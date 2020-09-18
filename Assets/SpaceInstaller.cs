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
        return new[]
        {
            new Commodity("Lobster", 100),
            new Commodity("Nice Gloves", 10),
            new Commodity("Space Helmets", 500),
            new Commodity("Star Fuel", 400),
            new Commodity("Food", 5),
            new Commodity("Alien Artifacts", 1000),
            new Commodity("Spacesuits", 600),
            new Commodity("Seeds", 50),
            new Commodity("Water", 30),
            new Commodity("Ore", 5),
            new Commodity("Rare Items", 2000)
        };
    }
}
