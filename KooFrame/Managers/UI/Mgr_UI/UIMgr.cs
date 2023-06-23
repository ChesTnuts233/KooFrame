using System;
using System.Collections;
using System.Collections.Generic;
using KooFrame.KooManagers.UIManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KooFrame
{
    public class UIMgr : Singleton<UIMgr>
    {
        public Canvas canvas; //画布
        public RectTransform canvasRectTransform; //记录UI的canvas  方便以后的使用
        public Transform canvasTransform; //画布的Transform

        private Transform bottom; //底层
        private Transform middle; //中层
        private Transform top; //顶层
        private Transform system; //系统层

        //存放各显示的面板的字典
        private Dictionary<string, BasePanel> panelsDic = new Dictionary<string, BasePanel>();

        public enum E_UI_Layer
        {
            Bottom,
            Top,
            Middle,
            System
        }

        public UIMgr()
        {
            //创建Canvas且过场景的时候不移除
            //GameObject obj = ResMgr.Instance.Load<GameObject>("UI/Canvas");
            GameObject obj = KooUIManager.Instance.GetKooCanvas();
            canvasRectTransform = obj.transform as RectTransform; //原框架这里的canvas是局部定义的 并没有使用到开头定义的公共变量,这里改为使用开头的公共变量
            canvasTransform = obj.transform;
            canvas = obj.GetComponent<Canvas>();
            //GameObject.DontDestroyOnLoad(obj);

            //找到各层
            bottom = canvasRectTransform.Find("Bottom");
            middle = canvasRectTransform.Find("Middle");
            top = canvasRectTransform.Find("Top");
            system = canvasRectTransform.Find("System");

            // //创建EvenSystem 并让其在过场景的时候不被移除
            // obj = ResMgr.Instance.Load<GameObject>("UI/EventSystem");
            // GameObject.DontDestroyOnLoad(obj);
        }

        public Transform GetLayerFather(E_UI_Layer layer)
        {
            switch (layer)
            {
                case E_UI_Layer.Bottom:
                    return bottom;
                case E_UI_Layer.Top:
                    return top;
                case E_UI_Layer.Middle:
                    return middle;
                case E_UI_Layer.System:
                    return system;
            }

            return null;
        }

        /// <summary>
        /// 打开面板 
        /// [1] 此方法曾用名为ShowMe()
        /// [2] 此方法用于实例化出面板，是否显示调用ShowMe或者直接于回调函数中调用
        /// [3] 注意 此方法包容ShowMe 但与现在的ShowMe区分开来 
        /// [4] ShowMe与HideMe用来管理是否把面板(通过位移或Alpha值)显示在游戏画面内，也请注意面板的射线检测交互是否关闭
        /// </summary>
        /// <typeparam name="T">面板脚本类型</typeparam>
        /// <param name="panelName">面板名</param>
        /// <param name="layer">显示在那一层</param>
        /// <param name="callBack">当面板预制体创建成功后 触发的事件</param>
        public void OpenPanel<T>(string panelName, E_UI_Layer layer = E_UI_Layer.Bottom, UnityAction<T> callBack = null,
            bool isShowPanel = true) where T : BasePanel
        {
            if (panelsDic.ContainsKey(panelName)) //如果面板已经存在，则直接调用面板显示出来后需要处理的函数
            {
                panelsDic[panelName].OpenMe();
                //处理面板创建完成后的逻辑
                if (callBack != null)
                {
                    callBack(panelsDic[panelName] as T);
                }

                return;
            }

            //把面板作为Canvas子对象
            //设置相对位置
            //找到父对象 到底显示在那一层
            ResMgr.Instance.LoadAsync<GameObject>("UI/UIPanel/" + panelName, (obj) =>
            {
                Transform father = bottom;
                switch (layer)
                {
                    case E_UI_Layer.Middle:
                        father = middle;
                        break;
                    case E_UI_Layer.Top:
                        father = top;
                        break;
                    case E_UI_Layer.System:
                        father = system;
                        break;
                }

                //设置父对象相对位置的大小
                obj.transform.SetParent(father);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;


                (obj.transform as RectTransform).offsetMax = Vector2.zero; //矩形右上角相对于右上角描点的偏移量
                (obj.transform as RectTransform).offsetMin = Vector2.zero; //矩形左下角相对于左下角描点的偏移量

                //得到预制体身上面板脚本
                T panel = obj.GetComponent<T>();
                //处理面板创建完成后的逻辑
                callBack?.Invoke(panel);
                panel.OpenMe();
                panel.name = panelName; //面板实名化(去除Clone)
                if (isShowPanel) //如果需要显示Panel 默认开启
                {
                    panel.ShowMe();
                }

                //把面板存起来
                try
                {
                    if (!IsHaveSomePanel(panelName))
                        panelsDic.Add(panelName, panel);
                    else Debug.Log("已存在当前面板，无法再次添加");
                }
                catch (Exception)
                {
                    ClearAllPanel();
                    Debug.Log("面板出现异常");
                }
            });
        }


        /// <summary>
        /// 关闭面板
        /// [1] 此方法曾用名为HideMe()
        /// [2] 此方法用于删除面板，是否显示调用HideMe或者直接于回调函数中调用
        /// [3] 注意 此方法包容HideMe 但与现在的HideMe区分开来 
        /// [4] ShowMe与HideMe用来管理是否把面板(通过位移或Alpha值)显示在游戏画面内，也请注意面板的射线检测交互是否关闭
        /// </summary>
        /// <param name="panelName">面板名</param>
        public void ClosePanel(string panelName)
        {
            if (panelsDic.ContainsKey(panelName))
            {
                panelsDic[panelName].CloseMe();
                GameObject.Destroy(panelsDic[panelName].gameObject);
                panelsDic.Remove(panelName);
            }
        }


        /// <summary>
        /// 得到某一个显示的面板 方便外部的使用
        /// </summary>
        /// <typeparam name="T">面板泛型</typeparam>
        /// <param name="name">面板名</param>
        /// <returns></returns>
        public T GetPanel<T>(string name) where T : BasePanel
        {
            if (panelsDic.ContainsKey(name))
            {
                return (T)panelsDic[name];
            }

            return null;
        }

        /// <summary>
        /// 自定义事件监听
        /// </summary>
        /// <param name="control">控件对象</param>
        /// <param name="type">事件类型</param>
        /// <param name="callBack">事件对应的相应函数</param>
        public void AddCustomEventListener(UIBehaviour control, EventTriggerType type,
            UnityAction<BaseEventData> callBack)
        {
            EventTrigger trigger = control.GetComponent<EventTrigger>();
            if (trigger != null)
                trigger = control.gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener(callBack);
            trigger.triggers.Add(entry);
        }

        /// <summary>
        /// 开启关闭检测
        /// </summary>
        /// <param name="panelName">面板名</param>
        /// <param name="isblock">是否开关检测</param>
        public void CanvasGroupControler(string panelName, bool isblock)
        {
            if (isblock == true)
            {
                panelsDic[panelName].UseMe();
            }
            else
            {
                panelsDic[panelName].DontUseMe();
            }
        }

        /// <summary>
        /// 判断面板是否存在
        /// </summary>
        /// <param name="panelName">面板名</param>
        public bool IsHaveSomePanel(string panelName)
        {
            if (panelsDic.ContainsKey(panelName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 清除所有Panel面板 
        /// </summary>
        public void ClearAllPanel()
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("UIPanel");
            foreach (GameObject panel in obj)
            {
                GameObject.Destroy(panel);
            }

            panelsDic.Clear();
        }

        ///<summary>
        /// 栈UI框架，用来实现多个UIPanel以栈的形式叠加
        /// </summary>
        private Stack<BasePanel> panelStack; //在系统层上存放Panel的栈  

        private Dictionary<UIPanelType, BasePanel> panelStackDic; //存储各面板身上的BasePanel组件


        //把某个页面入栈，同时把页面显示在界面上
        public void PushPanel(UIPanelType panelType, bool isShowMe = true)
        {
            if (panelStack == null)
            {
                panelStack = new Stack<BasePanel>();
            }

            //判断栈里面是否有页面
            if (panelStack.Count > 0)
            {
                BasePanel topPanel = panelStack.Peek(); //取出栈顶元素，但并不删除
                topPanel.OnPause();
            }

            BasePanel panel = GetPanel(panelType);
            panel.OpenMe();
            if (isShowMe)
            {
                panel.ShowMe();
            }

            //新页面入栈
            panelStack.Push(panel);
        }

        //根据面板类型得到实例化的面板
        private BasePanel GetPanel(UIPanelType panelType)
        {
            if (panelStackDic == null)
            {
                panelStackDic = new Dictionary<UIPanelType, BasePanel>();
            }

            BasePanel panel;
            panelStackDic.TryGetValue(panelType, out panel);
            if (panel == null)
            {
                //如果找不到，就找这个面板的预制体的路径，实例化该面板            
                GameObject instPanel =
                    (GameObject)GameObject.Instantiate(Resources.Load("UI/UIPanel/" + panelType.ToString()));
                instPanel.transform.SetParent(canvasTransform, false);
                panelStackDic.Add(panelType, instPanel.GetComponent<BasePanel>());
                return instPanel.GetComponent<BasePanel>();
            }
            else
            {
                return panel;
            }
        }

        //出栈 把页面从界面上移除
        public void PopPanel()
        {
            panelStack ??= new Stack<BasePanel>();
            if (panelStack.Count <= 0) return;
            //关闭栈顶页面的显示
            BasePanel topPanel = panelStack.Pop(); //移除栈顶元素 出栈
            topPanel.HideMe();
            if (panelStack.Count <= 0) return;
            BasePanel topPanel2 = panelStack.Peek(); //得到栈顶元素，但该元素不出栈
            topPanel2.OnResume();
        }
    }
}