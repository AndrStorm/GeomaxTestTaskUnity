using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public Player Player;
    public GameSettings GameSettings;
    
    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(Player).AsSingle();
        Container.Bind<InputHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
        Container.Bind<GameSettings>().FromInstance(GameSettings).AsSingle();
    }
}