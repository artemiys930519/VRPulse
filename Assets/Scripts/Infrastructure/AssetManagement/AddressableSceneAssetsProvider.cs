using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Infrastructure.AssetManagement
{
    public class AddressableSceneAssetsProvider : ISceneAssets, IProgress<float>
    {
        public event Action<float> LoadingSceneInPersentValue;
        public event Action SceneLoad;

        private Action _initializeComplete;
        private string _remotecatalogUrl;

        private SceneInstance _currentScene;

        public AddressableSceneAssetsProvider(string catalogPath, Action initializeComplete = default)
        {
            _initializeComplete = initializeComplete;
            _remotecatalogUrl = catalogPath;

            LoadCatalogAsync(_remotecatalogUrl);
        }

        private void Initialize()
        {
            Addressables.InitializeAsync();
        }

        public async void LoadCatalogAsync(string catalogPath)
        {
            var task = Addressables.LoadContentCatalogAsync(catalogPath).Task;

            await task;
            Initialize();
            _initializeComplete?.Invoke();
        }

        public async void LoadSceneAsync(string sceneName, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            _currentScene = await Addressables.LoadSceneAsync(sceneName, loadMode, false).ToUniTask(progress: this);
        }

        public void UnloadCurrentScene()
        {
            Addressables.UnloadSceneAsync(_currentScene).ToUniTask();
        }

        public void Cleanup()
        {
            Addressables.Release(_currentScene);
        }

        public void Report(float value)
        {
            LoadingSceneInPersentValue?.Invoke(value);
            
            if(value >= 1)
                SceneLoad?.Invoke();
        }
    }
}