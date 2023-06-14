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
            _factory = new Factory(
                new AddressableAssetsProvider("https://storage.yandexcloud.net/st-scenes/dev/bears", OnInitializeComplete));
        }

        private void OnInitializeComplete()
        {
        }
    }
}