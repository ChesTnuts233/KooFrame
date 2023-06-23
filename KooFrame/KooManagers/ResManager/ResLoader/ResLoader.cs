using System;
using System.Collections.Generic;
using KooFrame.KooManagers.ResManager;
using Object = UnityEngine.Object;

namespace KooFrame
{
    public class ResLoader
    {
        //当前资源加载记录
        private readonly List<Res> m_LoadedRecords = new List<Res>();

        /// <summary>
        /// 同步加载资源
        /// </summary>  
        /// <param name="assetPath">资源名（资源路径）</param>
        /// <typeparam name="T">资源类型</typeparam>
        /// <returns>加载到的资源</returns>
        public T LoadSync<T>(string assetPath) where T : Object
        {
            //1.查询本地和全局资源记录
            var res = GetResRecord(assetPath);
            if (res != null)
            {
                return res.Asset as T;
            }

            #region 同步加载资源

            //当前和全局都没有资源记录
            //2.建立资源
            res = CreateRes(assetPath);
            //3.同步加载资源
            res.LoadSync();

            return res.Asset as T;

            #endregion
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="assetPath">资源名（资源路径）</param>
        /// <param name="onLoaded">通过回调函数返回资源给用户</param>
        /// <typeparam name="T">资源的类型</typeparam>
        public void LoadASync<T>(string assetPath, Action<T> onLoaded) where T : Object
        {
            //查询本地和全局资源记录
            var res = GetResRecord(assetPath);
            if (res != null)
            {
                onLoaded(res.Asset as T);
                return;
            }

            #region 异步加载资源

            //当前和全局都没有资源记录
            //建立资源
            res = CreateRes(assetPath);
            //异步加载资源
            res.LoadAsync(loadedRes => { onLoaded(loadedRes.Asset as T); });

            #endregion
        }

        /// <summary>
        /// 释放所有资源
        /// </summary>
        public void ReleaseAll()
        {
            m_LoadedRecords.ForEach(loaderAssets => { loaderAssets.Release(); });
            m_LoadedRecords.Clear();
        }

        /// <summary>
        /// 获取资源从局部对象中查询
        /// </summary>
        /// <param name="assetPath">资源名（路径）</param>
        /// <returns>查询到的资源</returns>
        private Res GetResFormRecord(string assetPath)
        {
            return m_LoadedRecords.Find(loadedAssets => loadedAssets.Name == assetPath);
        }

        /// <summary>
        /// 创建资源
        /// </summary>
        /// <param name="assetPath">资源所在路径</param>
        /// <returns>被创建的资源</returns>
        private Res CreateRes(string assetPath)
        {
            Res res = null;
            if (assetPath.StartsWith("resources://") || assetPath.StartsWith("Resources://"))
            {
                var path = assetPath.Substring(12); //选择方式结束后消去前缀
                res = new ResourcesRes(path);
            }
            else
            {
                res = new AssetBundleRes(assetPath);
            }

            //var res = new Res(assetPath);    //res已经变为泛型

            //全局资源记录添加
            KooResMgr.Instance.GlobalLoadedRecords.Add(res);
            //添加本地资源记录
            AddResToRecord(res);
            return res;
        }

        /// <summary>
        /// 从本地对象和全局对象处查询获取资源
        /// </summary>
        /// <param name="assetPath">资源名（资源路径）</param>
        /// <returns>被查询到的资源</returns>
        private Res GetResRecord(string assetPath)
        {
            if (assetPath.StartsWith("resources://") || assetPath.StartsWith("Resources://"))
            {
                assetPath = assetPath.Substring(12);
            }

            #region 1.查询当前对象记录，判断在当前资源是否重复加载

            var res = GetResFormRecord(assetPath);

            if (res != null)
            { 
                return res; //当前有资源则返回此资源
            }

            #endregion

            #region 2.查询全局资源记录，判断全局资源是否重复加载

            //当前没有资源 查询全局资源池 查看资源是否重复加载
            res = KooResMgr.Instance.GetResFromGlobalRecord(assetPath);

            if (res != null) //全局另外一个对象加载了此资源
            {
                AddResToRecord(res);
                return res;
            }

            #endregion

            //3.都没查询到
            return null;
        }

        /// <summary>
        /// 添加资源到本地记录
        /// <para>资源计数加一</para>
        /// </summary>
        /// <param name="res">被记录的资源</param>
        private void AddResToRecord(Res res)
        {
            m_LoadedRecords.Add(res); //加入当前的资源记录
            res.Retain(); //计数
        }
    }
}