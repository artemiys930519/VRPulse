using UnityEditor;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class AssetPath
    {
        private static string _basePath = "";
        private static readonly string _catalogName = "";

        public static string GetRemoteCatalogPath() 
        {
            string targetPlatform = "";
#if UNITY_STANDALONE_WIN
            targetPlatform = "StandaloneWindows64";
#endif

#if UNITY_ANDROID
            targetPlatform = "Android";
#endif
#if UNITY_STANDALONE_LINUX
            targetPlatform = "StandaloneLinux64";
#endif
            return $"{_basePath}/{targetPlatform}/{_catalogName}";
        }
    }
}