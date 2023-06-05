using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.AssetManagement
{
    public class ScenesInBuildListAssetsProvider : ISceneAssets, IProgress<float>
    {
        public event Action<float> LoadingSceneInPersentValue;

        public event Action SceneLoad;

        private List<string> _currentSceneList = new();
        
        private Action _initializeComplete;

        public ScenesInBuildListAssetsProvider(Action initializeComplete = default)
        {
            _initializeComplete = initializeComplete;
            _initializeComplete?.Invoke();
        }

        public void LoadSceneAsync(string sceneName, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            if(!_currentSceneList.Contains(sceneName))
                _currentSceneList.Add(sceneName);
            
            SceneManager.LoadSceneAsync(sceneName, loadMode).ToUniTask(progress: this);
        }

        public void UnloadCurrentScene()
        {
            foreach (string scene in _currentSceneList)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }

        public void Cleanup()
        {
            UnloadCurrentScene();
        }

        public void Report(float value)
        {
            LoadingSceneInPersentValue?.Invoke(value);

            if (value >= 1)
            {
                SceneLoad?.Invoke();
            }
        }
    }
}