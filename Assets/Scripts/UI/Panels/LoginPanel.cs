using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels
{
    public class LoginPanel : ViewPanel
    {
        #region Inspector

        [SerializeField] private Button _enterButton;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private ApplicationBootstrap _entryPoint;

        #endregion

        private void OnEnable()
        {
            _enterButton.onClick.AddListener(()=> OnEnterButtonClick());
        }

        private void OnDisable()
        {
            _enterButton.onClick.RemoveListener(()=> OnEnterButtonClick());

        }

        private void OnEnterButtonClick()
        {
            _entryPoint.StartSceneWithName(_inputField.text);
        }
    }
}