using System.Collections.Generic;
using UnityEngine;

namespace KooFrame.MVC
{
    public abstract class View : MonoBehaviour
    {
        //名字标识
        public abstract string Name { get; }

        //事件关心列表
        [HideInInspector] //在面板隐藏
        public List<string> AttentionList = new List<string>();

        public virtual void RegisterAttentionEvent()
        {
            
        }
        ///<summary>
        /// 处理事件
        /// </summary>
        public abstract void HandleEvent(string name, object data);

        /// <summary>
        /// 发送事件
        /// </summary>
        protected void SendEvent(string eventName, object data = null)
        {
            MVC.SendEvent(eventName, data);
        }

        /// <summary>
        /// 获取模型
        /// </summary>
        protected Model GetModel<T>() where T : Model
        {
            return MVC.GetModel<T>();
        }
    }
}