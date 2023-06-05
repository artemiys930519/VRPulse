﻿using Shared;
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
            _playerName.OnValueChanged += OnValueChanged;
            
            SetUserNameServerRpc(new FixedString64Bytes(_clientData.UserName));
        }

        private void OnValueChanged(FixedString64Bytes previousValue, FixedString64Bytes newValue)
        {
            _playerNameTextField.text = newValue.Value;
        }

        [ServerRpc]
        private void SetUserNameServerRpc(FixedString64Bytes userName)
        {
            
            _playerName.Value = userName;
        }

    }
}