using Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class NetworkInstaller : MonoInstaller
    {
        [SerializeField] private NetworkFactory _networkFactory;
        
        public override void InstallBindings()
        {
            Container.Bind<NetworkFactory>().FromInstance(_networkFactory);
        }
    }
}