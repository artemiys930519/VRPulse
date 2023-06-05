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

        [SerializeField] private NetworkVariable<FixedString64Bytes> _playerName;
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
            
            if (!IsLocalPlayer)
                return;
                
            _playerName = new();
            _playerName.OnValueChanged += OnValueChanged;
            
                SetUserNameServerRpc(new FixedString64Bytes(_clientData.UserName));
        }

        public void SetPlayerName(string name)
        {
            _playerNameTextField.text = name;
        }

        private void OnValueChanged(FixedString64Bytes previousValue, FixedString64Bytes newValue)
        {
            if (IsLocalPlayer)
                SetPlayerName(newValue.Value);
            else
            {
                GetComponent<PlayerName>().SetPlayerName(_playerName.Value.Value);
            }
        }

        [ServerRpc(RequireOwnership = false)]
        private void SetUserNameServerRpc(FixedString64Bytes userName)
        {
            _playerName.Value = userName;
        }
    }
}