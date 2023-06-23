using System;
using KooFrame.KooManagers.ResManager;
using UnityEngine;

namespace KooFrame
{
    public class AssetBundleRes : Res
    {
        private string m_AssetsPath;
        private AssetBundle M_AssetBundle
        {
            get { return Asset as AssetBundle; }
            set { Asset = value; }
        }


        public AssetBundleRes(string assetPath)
        {
            m_AssetsPath = assetPath;
            Name = assetPath;
        }

        public override bool LoadSync()
        {
            return M_AssetBundle = AssetBundle.LoadFromFile(m_AssetsPath);
        }

        public override void LoadAsync(Action<Res> onLoaded)
        {
            var resRequest = AssetBundle.LoadFromFileAsync(m_AssetsPath);
            resRequest.completed += operation =>
            {
                M_AssetBundle = resRequest.assetBundle;
                onLoaded(this);
            };
        }

        protected override void OnReleaseRes()
        {
            if (M_AssetBundle != null) M_AssetBundle.Unload(true);
            KooResMgr.Instance.GlobalLoadedRecords.Remove(this);
            M_AssetBundle = null;
        }
    }
}