using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface IAssets
    {
        UniTask<GameObject> Instantiate(string path, Vector3 at);
        UniTask<GameObject> Instantiate(string path);
        void Cleanup();
        UniTask<T> Load<T>(string address) where T : class;
    }
}