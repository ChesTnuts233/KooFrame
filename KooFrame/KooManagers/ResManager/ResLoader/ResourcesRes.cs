using System;
using KooFrame.KooManagers.ResManager;
using UnityEngine;

namespace KooFrame
{
    public class ResourcesRes : Res
    {
        private string m_AssetsPath;
        public ResourcesRes(string assetPath)
        {
            m_AssetsPath = assetPath;
            Name = assetPath;
        }

        /// <summary>
        /// 同步加载
        /// </summary>
        /// <returns></returns>
        public override bool LoadSync()
        {
            return Asset = Resources.Load(m_AssetsPath);
        }

        public override void LoadAsync(Action<Res> onLoaded)
        {
            
            var resRequest = Resources.LoadAsync(m_AssetsPath);
            resRequest.completed += operation =>
            {
                Asset = resRequest.asset;
                onLoaded(this);
            };
        }

        protected override void OnReleaseRes()
        {
            if (Asset is GameObject)
            {
                Asset = null;
                Resources.UnloadUnusedAssets();
            }
            else
            {
                Resources.UnloadAsset(Asset);
            }

            KooResMgr.Instance.GlobalLoadedRecords.Remove(this);
            Asset = null;
        }
        
        
    }
}