using System.Collections;
using System.Collections.Generic;
using KooFrame;
using UnityEngine;
using UnityEngine.Events;

public class MonoMgr : Singleton<MonoMgr>
{
    private MonoController controller;

    public MonoMgr()
    {
        GameObject obj = new GameObject("MonoController");  //单例模式保证了MonoController对象的唯一性
        controller = obj.AddComponent<MonoController>();
        obj.transform.SetParent(GameRoot.Instance.transform);
    }


    public void AddUpdateListener(UnityAction fun)
    {
        controller.AddUpdateListener(fun);
    }

    public void RemoveUpdateListener(UnityAction fun)
    {
        controller.RemoveUpdateListener(fun);
    }
    //开启协程
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }
    //通过名字开启协程
    public Coroutine StartCoroutine(string methodName)
    {
        return controller.StartCoroutine(methodName);
    }
    //停止协程
    public void StopCoroutine(IEnumerator routine)
    {
        controller.StopCoroutine(routine);
    }

}
