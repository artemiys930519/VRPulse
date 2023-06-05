using Shared;
using Zenject;

namespace Installers
{
    public class CommonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IServerRepository>().To<ServerRepository>().AsSingle();
            Container.Bind<IClientData>().To<ClientData>().AsSingle();

        }
    }
}