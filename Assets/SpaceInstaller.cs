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
        Container.Bind<ISalary>().To<Salary>().AsSingle();
        Container.Bind<IBankAccount>().To<BankAccount>().AsSingle();
        Container.Bind<Text>().FromComponentSibling().AsTransient();
        Container.Bind<ITargetMarker>().FromComponentInNewPrefab(targetMarkerPrefab).AsSingle();
    }
}
