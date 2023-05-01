using VR.Player;
using VR.Player.Interfaces;
using Zenject;

namespace Installers
{
    public class OculusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IHandTrackingChecker>().To<OculusTrackingChecker>().AsTransient();
        }
    }
}