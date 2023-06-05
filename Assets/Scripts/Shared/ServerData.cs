using UnityEngine;

namespace Shared
{
    public class ServerData : IServerData
    {
        private const string IPKey = "IP";
        private const string PortKey = "Port";

        public string IP
        {
            get
            {
                if (PlayerPrefs.HasKey(IPKey))
                {
                    return PlayerPrefs.GetString(IPKey);
                }

                return string.Empty;
            }
            set => PlayerPrefs.SetString(IPKey, value);
        }

        public ushort Port
        {
            get
            {
                if (PlayerPrefs.HasKey(PortKey))
                {
                    return (ushort) PlayerPrefs.GetInt(PortKey);
                }

                return 0;
            }
            set => PlayerPrefs.SetInt(PortKey, value);
        }
    }
}