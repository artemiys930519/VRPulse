using DefaultNamespace;
using Unity.Netcode;
using UnityEngine;

namespace Network.User
{
    public class NetworkPlayer : NetworkBehaviour
    {
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private PlayerMovementSystem _movementSystem;

        public override void OnNetworkSpawn()
        {
            _playerCamera.gameObject.SetActive(IsLocalPlayer);
            _movementSystem.enabled = IsLocalPlayer;
        }
    }
}