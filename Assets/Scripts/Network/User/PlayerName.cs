using Shared;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Network.User
{
    public class PlayerName : NetworkBehaviour
    {
        #region Inspector

        [SerializeField] private NetworkVariable<FixedString64Bytes> _playerName = new();
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
            if (IsServer)
                return;

            _playerName.OnValueChanged += OnValueChanged;

            if (IsOwner)
                SetUserNameServerRpc(new FixedString64Bytes(_clientData.UserName));
            else
                SetPlayerName(_playerName.Value.Value);
        }

        public void SetPlayerName(string name)
        {
            _playerNameTextField.text = name;
        }

        private void OnValueChanged(FixedString64Bytes previousValue, FixedString64Bytes newValue)
        {                
            SetPlayerName(newValue.Value);
        }

        [ServerRpc]
        private void SetUserNameServerRpc(FixedString64Bytes userName)
        {
            _playerName.Value = userName;
        }
    }
}