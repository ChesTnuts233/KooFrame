using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace KooFrame
{
    public class SceneMgr : Singleton<SceneMgr>
    {
        public float aoProgress;
    
        /// <summary>
        /// 切换场景 同步
        /// </summary>
        /// <param name="name">场景名</param>
        /// <param name="callback">回调函数</param>
        public void LoadScene(string name, UnityAction callback = null)
        {
            SceneManager.LoadScene(name);       //场景同步加载
            callback?.Invoke();                 //切换后执行事件       
        }
        /// <summary>
        /// 提供给外部的 异步加载场景的接口方法
        /// </summary>
        /// <param name="name">场景名</param>
        /// <param name="callback">回调函数</param>
        public void LoadSceneAsy(string name, UnityAction callback = null)
        {
            //使用MonoMgr类帮助开启协程
            MonoMgr.Instance.StartCoroutine(ReallyLoadSceneAsy(name, callback));
        }
        /// <summary>
        /// 协程异步加载场景
        /// </summary>
        /// <param name="name">场景名</param>
        /// <param name="callback">回调函数</param>
        /// <returns></returns>
        private IEnumerator ReallyLoadSceneAsy(string name, UnityAction callback = null)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(name);
            //可以得到场景加载的进度
            while (!ao.isDone)
            {
                //事件中心 向外分发进度情况 外面想用就用
                EventCenter.Instance.EventTrigger<float>("进度条更新", ao.progress);
                yield return ao.progress;    //返回加载的进度
            }
            //aoProgress = ao.progress;
            callback();
        }
    }
}


