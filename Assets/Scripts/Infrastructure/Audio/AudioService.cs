using System;
using Agora.Rtc;
using DefaultNamespace.Infrastructure.Audio;
using UnityEngine;

namespace Infrastructure.Audio
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        #region Inspector

        [SerializeField] private AppIdInput _appIdInput;

        #endregion

        private string _appID = String.Empty;
        private string _audioChannel = String.Empty;
        private string _token = String.Empty;

        private IRtcEngine _mRtcEngine = null;

        public void Init()
        {
            _appID = _appIdInput.appID;
            _audioChannel = _appIdInput.channelName;
            _token = _appIdInput.token;

            InitRtcEngine();
        }

        public void StartConversation()
        {
            _mRtcEngine.JoinChannel(_token, _audioChannel);
        }

        private void InitRtcEngine()
        {
            _mRtcEngine = RtcEngine.CreateAgoraRtcEngine();
            RtcEngineContext context = new RtcEngineContext(_appID, 0,
                CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING,
                AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_DEFAULT);
            _mRtcEngine.Initialize(context);

            _mRtcEngine.EnableAudio();
            _mRtcEngine.SetChannelProfile(CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_COMMUNICATION);
            _mRtcEngine.SetClientRole(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);
        }
    }
}