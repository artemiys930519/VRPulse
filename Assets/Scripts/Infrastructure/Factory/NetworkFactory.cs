using Infrastructure.AssetManagement;
using Unity.Netcode;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class NetworkFactory : NetworkBehaviour
    {
        private IFactory _factory;
        private void Start()
        {
            //_factory = new Factory(
            //    new AddressableAssetsProvider(AssetPath.GetRemoteCatalogPath(), OnInitializeComplete));
        }

        private void OnInitializeComplete()
        {
            
        }
    }
}