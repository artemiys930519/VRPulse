using Data;
using Infrastructure.AssetManagement;
using Infrastructure.SceneLoader;
using Shared;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ApplicationBootstrap : MonoBehaviour
{
    private ISceneLoader _sceneLoader;

    private IClientData _clientData;
    
    [Inject]
    private void Construct(IClientData clientData)
    {
        _clientData = clientData;
    }

    private void Start()
    {
        _sceneLoader = new SceneLoader(new ScenesInBuildListAssetsProvider());
    }

    public void StartSceneWithName(string name)
    {
        _sceneLoader.LoadSceneAsync(Enumenators.SceneType.InfrastructureScene.ToString());
        _sceneLoader.LoadSceneAsync(Enumenators.SceneType.EnviromentScene.ToString(), LoadSceneMode.Additive);
        _clientData.UserName = name;
    }

    private void OnDisable()
    {
        _sceneLoader.Cleanup();
    }
}