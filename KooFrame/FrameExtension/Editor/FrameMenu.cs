#if UNITY_EDITOR

using KooFrame.BaseSystem;
using KooFrame.Test;
using UnityEditor;
using UnityEngine;

namespace KooFrame
{
    class FrameMenu
    {
        [MenuItem("KooFramework/生成框架UnityPackage")]
        private static void GeneratorFramePackage()
        {
            KooFrameInstance.GeneratorUnityPackage();
        }
        
        
        
        
    }
    
}
#endif