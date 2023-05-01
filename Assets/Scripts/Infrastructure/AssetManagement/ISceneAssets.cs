using System;
using UnityEngine.SceneManagement;

namespace Infrastructure.AssetManagement
{
    public interface ISceneAssets
    {
        public event Action<float> LoadingSceneInPersentValue;
        public event Action SceneLoad;
        
        public void LoadSceneAsync(string sceneName, LoadSceneMode loadMode = LoadSceneMode.Single);
        public void UnloadCurrentScene();
        public void Cleanup();
    }
}