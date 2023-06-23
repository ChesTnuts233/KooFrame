using KooFrame.BaseSystem;
using UnityEngine;
using UnityEditor;

namespace KooFrame
{
    [CustomEditor(typeof(UnityEditor.DefaultAsset))]
    public class ExDefaultAsset : Editor
    {
        public override void OnInspectorGUI()
        {
            string path = AssetDatabase.GetAssetPath(target);
            GUI.enabled = true;
            if (path.EndsWith(string.Empty))
            {
                if (path.Equals("Assets/KooFrame"))
                {
                    GUILayout.Label($"{KooFrameInstance.FrameInfo.Description}");
                    GUILayout.Label($"框架版本:{KooFrameInstance.FrameInfo.Version}");

                }
                if (path.Equals("Assets/KooFrame") && GUILayout.Button("输出框架名称"))
                {
                    Debug.Log(KooFrameInstance.GetFrameworkFileName(isAddVersion: true));
                }

                if (path.Equals("Assets/KooFrame") && GUILayout.Button("打包出框架UnityPackage"))
                {
                    KooUtils.CallMenuItem("生成框架UnityPackage");
                }
            }
        }
    }
}