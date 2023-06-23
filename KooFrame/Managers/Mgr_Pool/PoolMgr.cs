using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KooFrame
{
    /// <summary>
/// 缓存池模块（对象池）
/// </summary>
public class PoolMgr : Singleton<PoolMgr>   //单例
{
    //缓存池容器
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();
    private GameObject poolObj;  //对象池物体的根节点
    /// <summary>
    /// 往外拿东西
    /// </summary>
    /// <param name="pathName">路径名</param>
    /// <returns></returns>
    public GameObject GetObj(string pathName, UnityAction<GameObject> callBack)
    {
        GameObject obj = null;
        //Debug.Log(poolDic);
        if (poolDic.ContainsKey(pathName) && poolDic[pathName].poolList.Count > 0)
        {
            obj = poolDic[pathName].GetObj();  //拿到该物体
            callBack(obj);
        }
        else
        {
            //无资源管理模块
            //obj = GameObject.Instantiate(Resources.Load<GameObject>(pathName));  //实例化对象
            //obj.name = pathName;                                                 //改对象名字 使其与抽屉同名

            //有资源管理resmgr模块
            //使用异步加载资源 创建对象给外部使用
            ResMgr.Instance.LoadAsync<GameObject>(pathName, (objLA) =>
            {
                objLA.name = pathName;
                callBack(objLA);
            });
        }
        //PoolData类里已经包含
        //obj.SetActive(true);      //激活物体
        //obj.transform.parent = null;  //断开父子关系
        return obj;
    }

    /// <summary>
    /// 用不到的东西放入缓存池
    /// </summary>
    /// <param name="pathName">路径</param>
    /// <param name="obj">对象名</param>
    public void PushObj(string pathName, GameObject obj)
    {
        if (poolObj == null)
        {
            poolObj = new GameObject("Pool");
        }
        //obj.transform.parent = poolObj.transform;  //设置父对象为根节点
        //obj.SetActive(false);  //失活物体
        //如果有抽屉
        if (poolDic.ContainsKey(pathName))
        {
            poolDic[pathName].PushObj(obj);  //放入该物体
        }
        else  //如果无抽屉
        {
            poolDic.Add(pathName, new PoolData(obj, poolObj));  //申明一个抽屉并把该物体放入
        }
    }
    /// <summary>
    /// 清空缓存池 用于在切换场景
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
        //poolDic = null;
    }
}
}



