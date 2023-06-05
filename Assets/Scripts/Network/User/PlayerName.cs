using Shared;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Network.User
{
    public class PlayerName : NetworkBehaviour
    {
        #region Inspector

        [SerializeField] private NetworkVariable<string> _playerName = new();
        [SerializeField] private TMP_Text _playerNameTextField;
        #endregion
        
        private IClientData _clientData;

        [Inject]
        private void Construct(IClientData clientData)
        {
            _clientData = clientData;
        }

        public override void OnNetworkSpawn()
        {
            _playerName.OnValueChanged += OnValueChanged;
            
            SetUserNameServerRpc(_clientData.UserName);
        }

        [ServerRpc]
        private void SetUserNameServerRpc(string userName)
        {
            _playerName.Value = userName;
        }

        private void OnValueChanged(string previousValue, string newValue)
        {
            _playerNameTextField.text = newValue;
        }
    }
}