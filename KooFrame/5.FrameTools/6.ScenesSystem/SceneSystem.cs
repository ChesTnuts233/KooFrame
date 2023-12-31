﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace KooFrame
{
    public static class SceneSystem
    {
        /// <summary>
        /// 同步切换场景
        /// </summary>
        /// <param name="sceneName">场景名</param>
        /// <param name="mode">切换模式</param>
        /// <param name="callback">切换后执行的方法</param>
        public static void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single,
            UnityAction callback = null)
        {
            SceneManager.LoadScene(sceneName, mode);
            callback?.Invoke();
        }

        /// <summary>
        /// 同步切换场景
        /// </summary>
        /// <param name="sceneName">场景名</param>
        /// <param name="mode">切换模式</param>
        /// <param name="callback">切换后执行的方法</param>
        public static void LoadScene(int sceneBuildIndex, LoadSceneMode mode = LoadSceneMode.Single,
            UnityAction callback = null)
        {
            SceneManager.LoadScene(sceneBuildIndex, mode);
            callback?.Invoke();
        }

        /// <summary>
        /// 根据名称加载场景，并在场景加载完成后执行回调函数。
        /// </summary>
        /// <param name="sceneName">要加载的场景的名称。</param>
        /// <param name="loadSceneParameters">加载场景的参数。</param>
        /// <param name="callback">场景加载完成后要执行的回调函数。</param>
        /// <returns>已加载的场景对象。</returns>
        public static Scene LoadScene(string sceneName, LoadSceneParameters loadSceneParameters,
            UnityAction<Scene,LoadSceneMode> callback = null)
        {
            SceneManager.sceneLoaded += (scene, mode) => { callback?.Invoke(scene,mode); };
            return SceneManager.LoadScene(sceneName, loadSceneParameters);
        }

        public static Scene LoadScene(int sceneBuildIndex, LoadSceneParameters loadSceneParameters)
        {
            return SceneManager.LoadScene(sceneBuildIndex, loadSceneParameters);
        }

        /// <summary>
        /// 异步加载场景
        /// 可以选择EventSystem监听"LoadingSceneProgress"、"LoadSceneSucceed"等事件监听场景进度
        /// 也可以通过callBack参数
        /// </summary>
        /// <param name="sceneName">场景名称</param>
        /// <param name="callBack">回调函数,注意：每次进度更新都会调用一次,参数为0-1的进度</param>
        public static void LoadSceneAsync(string sceneName, Action<float> callBack = null,
            LoadSceneMode mode = LoadSceneMode.Single)
        {
            MonoSystem.Start_Coroutine(DoLoadSceneAsync(sceneName, callBack, mode));
        }

        private static IEnumerator DoLoadSceneAsync(string sceneName, Action<float> callBack = null,
            LoadSceneMode mode = LoadSceneMode.Single)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, mode);
            float progress = 0;
            while (progress < 1)
            {
                // 回调加载进度
                if (progress != ao.progress)
                {
                    progress = ao.progress;
                    callBack?.Invoke(progress);
                    // 把加载进度分发到事件中心
                    EventSystem.EventTrigger("LoadingSceneProgress", ao.progress);
                    if (progress == 1)
                    {
                        EventSystem.EventTrigger("LoadSceneSucceed");
                        break;
                    }
                }

                yield return CoroutineTool.WaitForFrames();
            }
        }
        

        

        
    }
}