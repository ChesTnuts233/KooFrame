using System.Collections.Generic;
using KooFrame.BaseSystem.Singleton;
using UnityEngine;

namespace KooFrame.KooManagers.ResManager
{
    public class KooResMgr : KooAutoSingletonMono<KooResMgr>
    {
        //全局资源加载记录 
        public readonly List<Res> GlobalLoadedRecords = new List<Res>();

        /// <summary>
        /// 从全局资源记录中查询获取资源
        /// </summary>
        /// <param name="assetPath">资源路径</param>
        /// <returns>被查询到的资源</returns>
        public Res GetResFromGlobalRecord(string assetPath)
        {
            return GlobalLoadedRecords.Find(loadedAssets => loadedAssets.Name == assetPath);
        }


#if UNITY_EDITOR
        private void OnGUI()
        {
            GUILayout.BeginVertical("box");
            GlobalLoadedRecords.ForEach(loadedRes =>
            {
                GUILayout.Label($"Name:{loadedRes.Name} RefCount:{loadedRes.RefCount}");
            });
            GUILayout.EndVertical();
        }
#endif
    }
}