using VR.Player;
using VR.Player.Interfaces;
using Zenject;

namespace Installers
{
    public class StandaloneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IHandTrackingChecker>().To<OculusTrackingChecker>().AsTransient();
        }
    }
}