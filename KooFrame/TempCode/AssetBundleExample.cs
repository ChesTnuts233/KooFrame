using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

namespace KooFrame
{
    public class AssetBundleExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("KooFramework/测试/AssetBundleExample/Build AssetBundle", false)]
        static void MenuItem1()
        {
            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath);
            }

            UnityEditor.BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath,
                UnityEditor.BuildAssetBundleOptions.None,
                UnityEditor.BuildTarget.StandaloneWindows);
        }

        [UnityEditor.MenuItem("KooFramework/测试/AssetBundleExample/Run", false)]
        static void MenuItem2()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("AssetBundleExample").AddComponent<AssetBundleExample>();
        }
#endif

        private AssetBundle m_Bundle;

        private void Start()
        {
            m_Bundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/gameobject");
            var gameobj = m_Bundle.LoadAsset<GameObject>("GameObject");
            Instantiate(gameobj);
        }
        private void OnDestroy()
        {
            m_Bundle.Unload(true);
        }
    }
}