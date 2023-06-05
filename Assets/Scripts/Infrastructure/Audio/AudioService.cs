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

        public IRtcEngine _mRtcEngine = null;

        private void Start()
        {
            Init();
            StartConversation();
        }

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
            UserEventHandler handler = new UserEventHandler(this);
            RtcEngineContext context = new RtcEngineContext(_appID, 0,
                CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING,
                AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_DEFAULT);
            _mRtcEngine.Initialize(context);

            _mRtcEngine.EnableAudio();
            _mRtcEngine.SetChannelProfile(CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_COMMUNICATION);
            _mRtcEngine.SetClientRole(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);
            
            _mRtcEngine.InitEventHandler(handler);
            
        }
    }
    #region -- Agora Event ---

    internal class UserEventHandler : IRtcEngineEventHandler
    {
        private readonly AudioService _audioSample;

        internal UserEventHandler(AudioService audioSample)
        {
            _audioSample = audioSample;
        }

        public override void OnError(int err, string msg)
        {
            Debug.Log(string.Format("OnError err: {0}, msg: {1}", err, msg));
        }

        public override void OnJoinChannelSuccess(RtcConnection connection, int elapsed)
        {
            int build = 0;
            Debug.Log(string.Format("sdk version: ${0}",
                _audioSample._mRtcEngine.GetVersion(ref build)));
            Debug.Log(
                string.Format("OnJoinChannelSuccess channelName: {0}, uid: {1}, elapsed: {2}",
                                connection.channelId, connection.localUid, elapsed));
        }

        public override void OnRejoinChannelSuccess(RtcConnection connection, int elapsed)
        {
            Debug.Log("OnRejoinChannelSuccess");
        }

        public override void OnLeaveChannel(RtcConnection connection, RtcStats stats)
        {
            Debug.Log("OnLeaveChannel");
        }

        public override void OnClientRoleChanged(RtcConnection connection, CLIENT_ROLE_TYPE oldRole, CLIENT_ROLE_TYPE newRole, ClientRoleOptions newRoleOptions)
        {
            Debug.Log("OnClientRoleChanged");
        }

        public override void OnUserJoined(RtcConnection connection, uint uid, int elapsed)
        {
            Debug.Log(string.Format("OnUserJoined uid: ${0} elapsed: ${1}", uid, elapsed));
        }

        public override void OnUserOffline(RtcConnection connection, uint uid, USER_OFFLINE_REASON_TYPE reason)
        {
            Debug.Log(string.Format("OnUserOffLine uid: ${0}, reason: ${1}", uid,
                (int)reason));
        }
    }

    #endregion
}