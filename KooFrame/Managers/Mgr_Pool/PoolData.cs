using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//抽屉数据 池子中的一列容器 （小池子）
namespace KooFrame
{
	public class PoolData
	{
		public GameObject fatherObj;         //抽屉中对象挂载的父节点
		public List<GameObject> poolList;    //对象的容器

		public PoolData(GameObject obj, GameObject poolObj)
		{
			//给抽屉创建一个父对象，并且把他作为对象池的子物体
			fatherObj = new GameObject(obj.name);
			fatherObj.transform.parent = poolObj.transform;

			poolList = new List<GameObject>();
			PushObj(obj);
		}
		/// <summary>
		/// 往抽屉里面放物体
		/// </summary>
		/// <param name="obj"></param>
		public void PushObj(GameObject obj)
		{
			obj.SetActive(false);    //物体失活
			poolList.Add(obj);       //存起来
			obj.transform.parent = fatherObj.transform;  //设置父对象
		}
		/// <summary>
		/// 往抽屉里面取东西
		/// </summary>
		/// <returns></returns>
		public GameObject GetObj()
		{
			GameObject obj = null;
			obj = poolList[0];  //取出第一个
			poolList.RemoveAt(0);
			obj.SetActive(true);
			obj.transform.parent = null; //断开父子关系
			return obj;
		}
	}
}

