using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infrastructure.AssetManagement
{
    public class AddressableAssetsProvider : IAssets
    {
        private string _remotecatalogUrl;
        private readonly Dictionary<string, AsyncOperationHandle> _completedCashe = new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();

        private Action _initializeComplete;

        public AddressableAssetsProvider(string catalogPath = "", Action initializeComplete = default)
        {
            _initializeComplete = initializeComplete;
            _remotecatalogUrl = catalogPath;

            if (string.IsNullOrEmpty(catalogPath))
                GetRemoteCatalogUrl();
            else
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

        public async UniTask<T> Load<T>(AssetReference assetReference) where T : class
        {
            if (_completedCashe.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(
              Addressables.LoadAssetAsync<T>(assetReference),
              cacheKey: assetReference.AssetGUID);
        }

        public async UniTask<T> Load<T>(string address) where T : class
        {
            if (_completedCashe.TryGetValue(address, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;
           
            return await RunWithCacheOnComplete(
              Addressables.LoadAssetAsync<T>(address),
              cacheKey: address);
        }

        public UniTask<GameObject> Instantiate(string address, Vector3 at)
        {
            return Addressables.InstantiateAsync(address, at, Quaternion.identity).Task.AsUniTask();
        }

        public UniTask<GameObject> Instantiate(string address) 
        {
            return Addressables.InstantiateAsync(address).Task.AsUniTask();
        }

        public void Cleanup()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
                foreach (AsyncOperationHandle handle in resourceHandles)
                    Addressables.Release(handle);
            
            _completedCashe.Clear();
            _handles.Clear();
        }

        private async UniTask<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += completeHandle =>
            {
                _completedCashe[cacheKey] = completeHandle;
            };

            AddHandle<T>(cacheKey, handle);

            return await handle.Task.AsUniTask();
        }

        private void AddHandle<T>(string key, AsyncOperationHandle handle) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }

        private void GetRemoteCatalogUrl()
        {
            LoadCatalogAsync(AssetPath.GetRemoteCatalogPath());
        }
    }
}