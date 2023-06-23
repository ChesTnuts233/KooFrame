using System.Collections.Generic;
using System;

namespace KooFrame.MVC
{
    /// <summary>
    /// 协调类
    /// </summary>
    public static class MVC
    {
        //资源
        public static Dictionary<string, Model> Models = new Dictionary<string, Model>(); //名字对应Model
        public static Dictionary<string, View> Views = new Dictionary<string, View>(); //名字对应View
        public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>(); //事件名对应类型


        //注册Model
        public static void RegisterModel(Model model)
        {
            Models[model.Name] = model;
        }

        //注册View
        public static void RegisterView(View view)
        {
            //防止重复注册
            if (Views.ContainsKey(view.Name)) 
            {
                Views.Remove(view.Name);
            }

            view.RegisterAttentionEvent(); //添加view前，直接注册关心事件
            Views[view.Name] = view;
        }

        //注册Controller
        public static void RegisterController(string eventName, Type controllerType)
        {
            CommandMap[eventName] = controllerType;
        }

        //获取Model
        public static T GetModel<T>() where T : Model
        {
            foreach (var m in Models.Values)
            {
                if (m is T)
                {
                    return (T)m;
                }
            }

            return null;
        }

        //获取View
        public static T GetView<T>() where T : View
        {
            foreach (var m in Views.Values)
            {
                if (m is T)
                {
                    return (T)m;
                }
            }

            return null;
        }

        //发送事件
        public static void SendEvent(string eventName, object data = null)
        {
            //controller执行
            if (CommandMap.ContainsKey(eventName))
            {
                Type type = CommandMap[eventName];
                //控制器生成
                Controller
                    controller =
                        Activator.CreateInstance(
                            type) as Controller; //虽然使用 new Controller() 也可以创建控制器实例，但是使用动态类型实例化更加符合 MVC 架构的设计原则。
                controller.Execute(data);
            }

            //一个View可以处理很多个事件
            foreach (var view in Views.Values)
            {
                if (view.AttentionList.Contains(eventName))
                {
                    //执行
                    view.HandleEvent(eventName, data);
                }
            }
        }
    }
}