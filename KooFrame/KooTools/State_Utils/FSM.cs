using System.Collections.Generic;
using UnityEngine;

namespace KooFrame.KooTools.State_Utils
{
    public class FSM
    {
        //当前状态
        private FSMState _curveState;

        /// <summary>
        /// 存储状态
        /// </summary>
        private Dictionary<string, FSMState> StatesDict = new Dictionary<string, FSMState>();

        /// <summary>
        /// 存储事件对应的跳转
        /// </summary>
        public Dictionary<string, FSMTranslation> TranslationsDic = new Dictionary<string, FSMTranslation>();

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state">添加的状态</param>
        public void AddState(FSMState state)
        {
            StatesDict[state.Name] = state;
        }

        /// <summary>
        /// 添加跳转
        /// </summary>
        public void AddTranslation(FSMTranslation translation)
        {
            TranslationsDic[translation.name] = translation;
        }

        /// <summary>
        /// 启动状态机
        /// </summary>
        /// <param name="state"></param>
        public void StartFSM(FSMState state)
        {
            _curveState = state;
        }

        public void HandleEvent(string name)
        {
            if (_curveState != null && TranslationsDic.ContainsKey(name))
            {
                Debug.Log("FromState" + _curveState.Name);

                TranslationsDic[name].callFunc();
                //当前状态转换为目标状态
                _curveState = TranslationsDic[name].ToState;
                Debug.Log("toState" + _curveState.Name);
            }
        }
    }
}