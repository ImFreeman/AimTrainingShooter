using Zenject;

namespace Features.Input
{
    public class InputInstaller : Installer<InputInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ICameraRotationInput>()
                .To<MousePositionInput>()
                .AsSingle();
            Container
                .Bind<IFireButtonInput>()
                .To<MouseButtonInput>()
                .AsSingle();
        }
    }
}