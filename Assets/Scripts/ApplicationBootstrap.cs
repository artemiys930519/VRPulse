using Data;
using Infrastructure.AssetManagement;
using Infrastructure.SceneLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationBootstrap : MonoBehaviour
{
    private ISceneLoader _sceneLoader;
    private void Start()
    {
        _sceneLoader = new SceneLoader(new ScenesInBuildListAssetsProvider());
        _sceneLoader.LoadSceneAsync(Enumenators.SceneType.NetworkInfrastructureScene.ToString());
        _sceneLoader.LoadSceneAsync(Enumenators.SceneType.EnviromentScene.ToString(), LoadSceneMode.Additive);
    }

    private void OnDisable()
    {
        _sceneLoader.Cleanup();
    }
}