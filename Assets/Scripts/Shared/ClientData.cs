using UnityEngine;

namespace Shared
{
    public class ClientData : IClientData
    {
        private const string NameKey = "UserName";
        
        public string UserName {
            get
            {
                if (PlayerPrefs.HasKey(NameKey))
                {
                    return PlayerPrefs.GetString(NameKey);
                }

                return string.Empty;
            }
            set => PlayerPrefs.SetString(NameKey, value);
        }
    }
}