using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class Factory : IFactory
    {
        private IAssets _assets;

        public Factory(IAssets assets) 
        {
            _assets = assets;
        }
        
        public async UniTask<GameObject> CreateObject(string itemName)
        {
            return await _assets.Instantiate(itemName, Vector3.zero);
        }

        public async UniTask<GameObject> LoadObject(string itemName) 
        {
            return await _assets.Load<GameObject>(itemName);
        }

        public void CleanUp() 
        {
            _assets.Cleanup();
        }
    }
}
