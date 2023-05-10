using System;
using System.Collections.Generic;
using UnityEngine;
using VR.Player.Interfaces;
using Zenject;

namespace VR.Player
{
    public class XrMetaPlayer : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private List<PositionSwitcherData> _handInteractorGameObjects = new();

        #endregion

        private IHandTrackingChecker _handTrackingChecker;
        private bool _handTrackingActiveState;

        [Inject]
        private void Construct(IHandTrackingChecker handTrackingChecker)
        {
            _handTrackingChecker = handTrackingChecker;
        }

        void Start()
        {
            foreach (PositionSwitcherData switcherData in _handInteractorGameObjects)
            {
                switcherData.LocalStartPosition = switcherData.SwitcherGameObject.transform.localPosition;
            }
        }

        void Update()
        {
            if (_handTrackingActiveState == _handTrackingChecker.IsHandTrackingEnable())
                return;
        
            _handTrackingActiveState = _handTrackingChecker.IsHandTrackingEnable();

            SwitchHandInteractorPosition(_handTrackingActiveState);
        }

        private void SwitchHandInteractorPosition(bool isSwitchPosition)
        {
            for (int i = 0; i < _handInteractorGameObjects.Count; i++)
            {
                PositionSwitcherData tempSwitcherData = _handInteractorGameObjects[i];

                tempSwitcherData.SwitcherGameObject.transform.localPosition = isSwitchPosition
                    ? tempSwitcherData.LocalCorrectionPosition
                    : tempSwitcherData.LocalStartPosition;
            }
        }

        #region InnerClass

        [Serializable]
        public class PositionSwitcherData
        {
            public Vector3 LocalCorrectionPosition;
            public GameObject SwitcherGameObject;

            public Vector3 LocalStartPosition
            {
                get { return _localStartPosition; }
                set { _localStartPosition = value; }
            }

            private Vector3 _localStartPosition;
        }

        #endregion
    }
}