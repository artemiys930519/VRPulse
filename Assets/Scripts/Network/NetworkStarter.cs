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
        
        private IServerRepository _serverRepository;

        [Inject]
        private void Construct(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        private void Start()
        {
            _networkManager = GetComponent<NetworkManager>();
            _unityTransport = GetComponent<UnityTransport>();
            
            ServerData serverData = _serverRepository.GetServerInfo();
            
            SetConnectionInfo(serverData.IP,serverData.Port);
            
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