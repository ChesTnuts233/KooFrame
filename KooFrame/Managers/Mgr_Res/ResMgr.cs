using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*资源加载模块
 1.异步加载
 2.委托和lambda表达式
 3.协程
 4.泛型
 */
public class ResMgr : Singleton<ResMgr>
{
	//同步加载资源
	public T Load<T>(string name, UnityAction callBack = null) where T : Object
	{
		T res = Resources.Load<T>(name);
		if (res is GameObject)
		{   //在这里写回调函数我也不知是否是正确的 后日再调整吧
			callBack?.Invoke();
			return GameObject.Instantiate(res);
		}
		else
		{
			return res;
		}
	}

	//异步加载资源
	public void LoadAsync<T>(string name, UnityAction<T> callBack = null) where T : Object
	{
		//开启异步加载的协程
		MonoMgr.Instance.StartCoroutine(ReallyLoadAsync(name, callBack));
	}

	//真正的协程程序函数 用于开启异步加载对应的资源
	private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callBack = null) where T : Object
	{
		ResourceRequest r = Resources.LoadAsync<T>(name);
		yield return r;
		//加载完成后
		if (r.asset is GameObject)
		{
			callBack(GameObject.Instantiate(r.asset) as T);
		}
		else
		{
			callBack(r.asset as T);
		}
	}
}
