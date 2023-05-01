using Infrastructure.AssetManagement;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ISceneAssets _sceneAssets;
        
        public SceneLoader(ISceneAssets sceneAssets)
        {
            _sceneAssets = sceneAssets;
        }
        
        public void LoadSceneAsync(string sceneName, LoadSceneMode loadMode = LoadSceneMode.Single)
        { 
            _sceneAssets.LoadSceneAsync(sceneName, loadMode);
        }

        public void UnloadCurrentScene()
        {
            _sceneAssets.UnloadCurrentScene();
        }

        public void Cleanup()
        {
            _sceneAssets.Cleanup();
        }
    }
}