using Shared;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using Zenject;

namespace Network
{
    public class NetworkStarter : MonoBehaviour
    {
        private NetworkManager _networkManager;
        private UnityTransport _unityTransport;
        
        private IServerData _serverData;

        [Inject]
        private void Construct(IServerData serverData)
        {
            _serverData = serverData;
        }

        private void Start()
        {
            _networkManager = GetComponent<NetworkManager>();
            _unityTransport = GetComponent<UnityTransport>();
            
            SetConnectionInfo(_serverData.IP,_serverData.Port);
            
#if UNITY_SERVER
        ServerConnection();
#endif
            
#if !UNITY_SERVER
            ClientConnection();
#endif
        }

        private void ClientConnection()
        {
            _networkManager.StartClient();
        }

        private void ServerConnection()
        {
            _networkManager.StartServer();
        }

        private void SetConnectionInfo(string IP, ushort port)
        {
            _unityTransport.ConnectionData.Port = port;
            _unityTransport.ConnectionData.Address = IP;
        }
    }
}