using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KooFrame
{
    /*面板基类
 找到所有自己面板下的控件对象 提供显示或隐藏的行为*/
    public abstract class BasePanel : MonoBehaviour
    {
        private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();


        protected virtual void Awake()
        {
            FindChildrenControl<Button>();
            FindChildrenControl<Image>();
            FindChildrenControl<Text>();
            FindChildrenControl<Toggle>();
            FindChildrenControl<Slider>();
        }

        /// <summary>
        /// 找到子对象对应控件
        /// </summary>
        /// <typeparam name="T">控件泛型种类</typeparam>
        private void FindChildrenControl<T>() where T : UIBehaviour
        {
            T[] controls = this.GetComponentsInChildren<T>(); //会找到该游戏物体下所有的T类型的组件

            for (int i = 0; i < controls.Length; i++)
            {
                string objName = controls[i].gameObject.name;
                objName = controls[i].gameObject.name;
                if (controlDic.ContainsKey(objName))
                {
                    controlDic[objName].Add(controls[i]);
                }
                else
                {
                    controlDic.Add(objName, new List<UIBehaviour>() { controls[i] });
                }

                if (controls[i] is Button) //当控件是Button
                {
                    (controls[i] as Button).onClick.AddListener(() => { OnClick(objName); });
                }

                if (controls[i] is Toggle) //当控件是Toggle
                {
                    (controls[i] as Toggle).onValueChanged.AddListener((value) => { OnValueChange(objName, value); });
                }
            }
        }

        /// <summary>
        /// 得到对应的控件脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controlName"></param>
        /// <returns></returns>
        protected T GetControl<T>(string controlName) where T : UIBehaviour
        {
            if (controlDic.ContainsKey(controlName))
            {
                for (int i = 0; i < controlDic[controlName].Count; i++)
                {
                    if (controlDic[controlName][i] is T)
                    {
                        return controlDic[controlName][i] as T;
                    }
                }
            }

            return null;
        }

        public virtual void OpenMe()
        {
        } //界面开启 

        public virtual void CloseMe()
        {
        } //界面关闭

        public virtual void ShowMe()
        {
        } //界面显示

        public virtual void HideMe()
        {
        } //界面消隐

        public virtual void UseMe()
        {
        } //界面开启交互

        public virtual void DontUseMe()
        {
        } //界面关闭交互

        public virtual void OnPause()
        {
        } //页面暂停

        public virtual void OnResume()
        {
        } //界面继续

        protected virtual void OnClick(string btnName)
        {
        } //按钮点击

        protected virtual void OnValueChange(string toggleName, bool value)
        {
        } //toggle值改变
    }
}