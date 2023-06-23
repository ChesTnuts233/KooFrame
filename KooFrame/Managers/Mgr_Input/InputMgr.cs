using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KooFrame
{
    public class InputMgr : Singleton<InputMgr>
    {
        private bool isStart = false;

        /// <summary>
        /// 是否开启输入检测
        /// </summary>
        /// <param name="isOpen"></param>
        public void StartOrEndCheck(bool isOpen)
        {
            isStart = isOpen;
        }

        public InputMgr()
        {
            //构造函数中添加Update监听
            MonoMgr.Instance.AddUpdateListener(MyUpdate);
        }

        private void MyUpdate()
        {
            //没有开启输入检测 就停止检测 直接return
            if (!isStart)
            {
                return;
            }

            //Debug.Log("Checking");
            CheckKeyCode(KeyCode.W);
            CheckKeyCode(KeyCode.A);
            CheckKeyCode(KeyCode.S);
            CheckKeyCode(KeyCode.D);
            CheckKeyCode(KeyCode.Tab);
            CheckKeyCode(KeyCode.G);
            CheckKeyCode(KeyCode.T);
            CheckKeyCode(KeyCode.Y);
            CheckKeyCode(KeyCode.U);
            CheckKeyCode(KeyCode.P);
        }

        /// <summary>
        /// 检测按键抬起按下 分发的事件
        /// </summary>
        /// <param name="key"></param>
        private void CheckKeyCode(KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                EventCenter.Instance.EventTrigger<object>("某键按下", key);
            }

            if (Input.GetKeyUp(key))
            {
                EventCenter.Instance.EventTrigger<object>("某键抬起", key);
            }
        }
    }
}