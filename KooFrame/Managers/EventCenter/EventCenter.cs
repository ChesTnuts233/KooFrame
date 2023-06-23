using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KooFrame
{
    public class EventCenter : Singleton<EventCenter>
    {
        public interface IEventInfo
        {
        } // 事件信息的接口

        public class EventInfo<T> : IEventInfo //事件信息泛型类
        {
            public UnityAction<T> actions;

            public EventInfo(UnityAction<T> action)
            {
                actions += action;
            }
        }

        public class EventInfo : IEventInfo // 事件信息类
        {
            public UnityAction actions;

            public EventInfo(UnityAction action)
            {
                actions += action;
            }
        }

        //key--事件名字 value--对应 监听这个事件 对应的接口
        private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

        private Dictionary<string, UnityAction<object>>
            eventDicObj = new Dictionary<string, UnityAction<object>>(); //不使用泛型的事件字典 已经弃用

        #region 事件添加

        /// <summary>
        /// 添加事件监听
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="action">准备用来处理事件的委托函数</param>
        /// <param name="isRepeatAdd">是否可以重复添加事件</param>
        public void AddEventListener<T>(string name, UnityAction<T> action, bool isRepeatAdd = false)
        {
            //有没有对应的事件监听
            //有的情况
            if (eventDic.ContainsKey(name))
            {
                if (isRepeatAdd == true)
                {
                    (eventDic[name] as EventInfo<T>).actions += action;
                    Debug.Log("你重复添加了一个事件");
                }
            }
            //没有的情况
            else
            {
                eventDic.Add(name, new EventInfo<T>(action));
            }
        }

        public void AddEventListener(string name, UnityAction action)
        {
            if (eventDic.ContainsKey(name)) //有对应的监听事件
            {
                //Debug.Log("事件已经存在");
                //(eventDic[name] as EventInfo).actions = action;    //注册事件
            }
            else
            {
                eventDic.Add(name, new EventInfo(action));
                //(eventDic[name] as EventInfo).actions += action;    //注册事件
                //Debug.Log("事件进行注册了");
            }
        }

        #region 已弃用

        //已弃用
        //public void AddEventListenerObj(string name, UnityAction<object> action)//可用
        //{
        //    if (eventDic.ContainsKey(name))
        //    {
        //        eventDicObj[name] += action;
        //    }
        //    else
        //    {
        //        eventDicObj.Add(name, action);
        //    }
        //}

        #endregion

        #endregion

        #region 事件触发

        //// <summary>
        /// 事件触发
        /// </summary>
        /// <param name="name">哪一个名字的事件触发了</param>
        public void EventTrigger<T>(string name, T info)
        {
            //有没有对应的事件监听
            //有的情况
            if (eventDic.ContainsKey(name))
            {
                //eventDic[name]();
                if ((eventDic[name] as EventInfo<T>).actions != null)
                    (eventDic[name] as EventInfo<T>).actions.Invoke(info);
                //eventDic[name].Invoke(info);
            }
        }

        public void EventTrigger(string name)
        {
            if (eventDic.ContainsKey(name)) //有对应的事件监听
            {
                (eventDic[name] as EventInfo).actions?.Invoke(); //执行相应的委托  
            }
        }

        public void EventTriggerObj(string name, object info)
        {
            if (eventDicObj.ContainsKey(name))
            {
                eventDicObj[name](info);
            }
        }

        #endregion

        #region 事件移除

        /// <summary>
        /// 移除对应的事件监听
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">事件名</param>
        /// <param name="action">对应之前添加的委托函数</param>
        public void RemoveEventListener<T>(string name, UnityAction<T> action)
        {
            if (eventDic.ContainsKey(name))
                (eventDic[name] as EventInfo<T>).actions -= action; //移除事件
        }

        public void RemoveEventListener(string name, UnityAction action)
        {
            if (eventDic.ContainsKey(name))
                (eventDic[name] as EventInfo).actions -= action; //移除事件
        }

        #endregion
    }
}