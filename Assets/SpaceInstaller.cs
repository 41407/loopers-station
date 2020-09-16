using Zenject;

public class SpaceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ISteerable>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IKeybindings>().To<DefaultKeybindings>().AsSingle();
    }
}
