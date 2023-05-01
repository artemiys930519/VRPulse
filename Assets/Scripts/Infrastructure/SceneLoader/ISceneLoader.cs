using UnityEngine.SceneManagement;

namespace Infrastructure.SceneLoader
{
    public interface ISceneLoader
    {
        public void LoadSceneAsync(string sceneName, LoadSceneMode loadMode = LoadSceneMode.Single);
        public void UnloadCurrentScene();
        public void Cleanup();
    }
}