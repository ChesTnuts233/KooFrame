using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单例类
/// </summary>
/// <typeparam name="T">泛型</typeparam>
public class Singleton<T> where T : new()  //泛型约束  必须有无参构造函数
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = new T();
            return instance;
        }
    }
    //private Singleton()  //可以省略的构造函数
    //{

    //}
}