using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IFactory
    {
        UniTask<GameObject> CreateObject(string itemName);
        UniTask<GameObject> LoadObject(string itemName);
    }
}
