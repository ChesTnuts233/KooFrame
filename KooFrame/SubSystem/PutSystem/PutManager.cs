// using System.Collections;
// using System.Collections.Generic;
// using KooFrame;
// using UnityEngine;
//
//
// public class PutManager : Singleton<PutManager>
// {
// 	//public string PutObjName;
// 	//public Dictionary<string, GameObject> PutObjDic = new Dictionary<string, GameObject>();
//
// 	public GameObject PutObj;      //放置物体
// 	public GameObject PutPos;      //放置位置
//
//
// 	[SerializeField] private Vector3 ScreenCenterPos;      //屏幕坐标
//
//
// 	public float StartDistance = 5f;                  //放置的物体与摄像机之间的距离
//
// 	[Header("是否开启放置模式")][SerializeField] private bool isPuttingModel = false;
// 	public bool isShowModel = false;
//
// 	public PutManager()
// 	{
// 		ScreenCenterPos = new Vector3(Screen.width / 2, Screen.height / 2, StartDistance);
// 		//构造函数中添加Update监听
// 		MonoMgr.Instance.AddUpdateListener(PutManagerUpdate);
// 		//Debug.Log("PutManager");
// 	}
//
// 	private void PutManagerUpdate()
// 	{
// 		EnterPutMode();
// 		PutObjInWorld();
// 	}
// 	/// <summary>
// 	/// 放置模式，按下相应的按键后进入放置模式，放置模式中，放置预览体销毁后放置体被放下
// 	/// </summary>
// 	private void EnterPutMode()
// 	{
// 		if (Input.GetKeyDown(KeyCode.E) && isPuttingModel == false)
// 		{
// 			Debug.Log("打开放置模式");
// 			isPuttingModel = true;                                         //开启放置模式
//
// 			//调出放置物的父物体
// 			PoolMgr.Instance.GetObj("PutSystem/PutPreview", (obj) =>
// 			{
// 				PutPos = obj;
// 				obj.transform.position = Camera.main.ScreenToWorldPoint(ScreenCenterPos);
//
// 			});
// 			//调出放置物体
// 			PoolMgr.Instance.GetObj("PutSystem/PutTest", (obj) =>
// 			{
// 				PutObj = obj;
// 				PutObj.transform.parent = PutPos.transform;
// 				PutObj.transform.localPosition = Vector3.zero;
// 			});
//
// 		}
// 	}
//
//
// 	private void PutObjInWorld()
// 	{
// 		if (isPuttingModel)             //如果是处于放置模式
// 		{
// 			if (PutPos != null)     //放置体不为空时
// 			{
// 				Vector3 putPos = Camera.main.ScreenToWorldPoint(ScreenCenterPos);
// 				PutPos.transform.position = new Vector3(Mathf.Ceil(putPos.x), 5.44f, Mathf.Ceil(putPos.z));
// 			}
// 			if (Input.GetKeyDown(KeyCode.Mouse1))
// 			{
// 				isPuttingModel = false;
// 				PutPos = null;
// 			}
// 		}
//
// 	}
//
// }
