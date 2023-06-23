using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Defective.JSON;
using UnityEngine.SceneManagement;
using Imdork.Mysql;
using KooFrame;
using KooFrame.BaseSystem;
using KooFrame.KooManagers.UIManager;

/// <summary>
/// 
/// </summary>
public class InventoryManager : MonoBehaviour
{
	#region 单例模式
	private static InventoryManager instance;
	public static InventoryManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
			}
			return instance;
		}
	}
	#endregion
	//private GameObject[] dontDestoryObj;                            //不销毁的物品


	[SerializeField] private bool isPickedItem;
	public bool IsPickedItem
	{
		get { return isPickedItem; }
	}

	[SerializeField] private ItemUI pickedItem;                                      //鼠标选中的物体
	public ItemUI PickedItem
	{
		get { return pickedItem; }
		set { pickedItem = value; }
	}


	//public GameObject ShowObject;

	private List<Item> itemList;											//物品信息列表集合
	public Canvas canvas;													//获得画布

	#region ToolTipData
	public ToolTipPanel toolTipPanel;										//提示面板
	private Vector2 toolTipPositionOffset = new Vector2(80, -90);       //位置偏移
	private bool isToolTipShow = false;										//提示开关
	#endregion


	//public ItemData test;
	
	

	#region Unity生命周期
	void Awake()
	{
		//ParseItemJson();														//开始时解析JSON物品信息
		//UseMySQl();															//使用数据库解析物品数据
		//GameObject.DontDestroyOnLoad(this.gameObject);
		//canvas = KooUIManager.Instance.GetKooCanvas().GetComponent<Canvas>();
		//ParseScriptableObject();
		
		
		
		ToolTipPanel tipobj = Resources.Load<ToolTipPanel>("UI/UIPanel/ToolTipPanel");
		toolTipPanel = tipobj;
		// UIMgr.Instance.OpenPanel<InventoryPanel>("InventoryPanel", UIMgr.E_UI_Layer.Top, (panel) =>
		// {
		// 	panel.transform.localPosition = new Vector2(0, 0);
		// 	PickedItem = GameObject.Find("PickedItem").GetComponent<ItemUI>();
		// }, false);
	}
	void Start()
	{
		//var tempPickedItem = canvas.transform.Find("InventoryPanel/PickedItem");
		//pickedItem = tempPickedItem.GetComponent<ItemUI>();
		//pickedItem.Hide();
		//canvas = GameObject.Find("Canvas").GetComponent<Canvas>();      //获得画布
	}
	
	
	private void Update()
	{
		if (isPickedItem)       //如果捡起了物品 让物品跟随鼠标
		{
			Vector2 position;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
			pickedItem.SetLocalPosition(position);
		}
		if (isToolTipShow)     //控制提示面板跟随鼠标
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out Vector2 position);    //此处内敛了位置坐标的声明
																																							//Debug.Log(position);
			toolTipPanel.SetLocalPosition(position + toolTipPositionOffset);
		}
		if (isPickedItem && Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1) == false)   //left mouse button" (pointerId = -1)
		{
			//PutShowObject();
			isPickedItem = false;
			PickedItem.Hide();
		}


	}
	#endregion


	public void UseMySQl()
	{
		itemList = new List<Item>();
		//创建数据库类                   IP地址       端口    用户名   密码    数据库项目名称
		var mySqlTools = new SqlHelper("127.0.0.1", "3306", "root", "0000", "user");
		//打开数据库
		mySqlTools.Open();
		//查询方法                          表名        查询字段名        判断字段名       判断符号        条件成立数据
		var name = mySqlTools.SelectWhere("item", new[] { "name" }, new[] { "id" }, new[] { "=" }, new[] { "1" });
		var type = mySqlTools.SelectWhere("item", new[] { "type" }, new[] { "id" }, new[] { "=" }, new[] { "1" });
		var description = mySqlTools.SelectWhere("item", new[] { "description" }, new[] { "id" }, new[] { "=" }, new[] { "1" });
		var capacity = mySqlTools.SelectWhere("item", new[] { "capacity" }, new[] { "id" }, new[] { "=" }, new[] { "1" });
		var sprite = mySqlTools.SelectWhere("item", new[] { "sprite" }, new[] { "id" }, new[] { "=" }, new[] { "1" });
	
	
		//SelectWhere方法会返回Dataset类对象， 声明ds变量接收如上图
		//                                 方法第一个参数   方法第二个参数
		//调用MysqlTools 工具类             Dataset类对象  查询字段
	
		//以下解析这个对象里面的公有属性
		string typeStr = (string)MysqlTools.GetValue(type, "type");
		//将存的字符串转化为枚举
		ItemType itemType = (ItemType)Enum.Parse(typeof(ItemType), typeStr);
		//int id = (int)1;
		string itemName = (string)MysqlTools.GetValue(name, "name");
		string itemDescription = (string)MysqlTools.GetValue(description, "description");
		int itemCapacity = (int)MysqlTools.GetValue(capacity, "capacity");
		string itemSprite = (string)MysqlTools.GetValue(sprite, "sprite");
		mySqlTools.Close();
		// //获取子类特有的属性
		// Item itemObj = null;
		// switch (itemType)
		// {
		// 	case ItemType.Model:
		// 		itemObj = new Item_Model(id, itemName, itemType, itemDescription, itemCapacity, itemSprite);
		// 		break;
		// 	case ItemType.Character:
		// 		itemObj = new Item_Character(id, itemName, itemType, itemDescription, itemCapacity, itemSprite);
		// 		break;
		// }
		// itemList.Add(itemObj);
	}

	/// <summary>
	/// 解析物品信息
	/// </summary>
	void ParseItemJson()
	{
		itemList = new List<Item>();

		//文本在Unity中是TextAsset类型
		TextAsset itemText = Resources.Load<TextAsset>("Items");

		//物品信息的Json格式
		string itemsJson = itemText.text;

		JSONObject j = new JSONObject(itemsJson);

		//遍历JSON文件里的物品
		foreach (JSONObject item in j.list)
		{
			string typeStr = item["type"].stringValue;
			//将json文件中存的字符串转化为枚举
			ItemType type = (ItemType)Enum.Parse(typeof(ItemType), typeStr);

			//以下解析这个对象里面的公有属性
			int id = (int)(item["id"].intValue);
			string name = item["name"].stringValue;
			ItemQuality quality = (ItemQuality)Enum.Parse(typeof(ItemQuality), item["quality"].stringValue);
			string description = item["description"].stringValue;
			int capacity = (int)(item["capacity"].intValue);
			int buyPrice = (int)(item["buyPrice"].intValue);
			int sellPrice = (int)(item["sellPrice"].intValue);

			string sprite = item["sprite"].stringValue;

			//获取子类特有的属性
			Item itemObj = null;
			switch (type)
			{
				case ItemType.Consumable:
					int hp = item["hp"].intValue;
					int mp = item["mp"].intValue;
					itemObj = new Item_Consumable(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, hp, mp);
					break;
				case ItemType.Equipment:
					int strength = (int)(item["strength"].intValue);
					int intellect = (int)(item["intellect"].intValue);
					int agility = (int)(item["agility"].intValue);
					int stamina = (int)(item["stamina"].intValue);
					EquipmentType equipType = (EquipmentType)Enum.Parse(typeof(EquipmentType), item["equipType"].stringValue);
					itemObj = new Item_Equipment(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, strength, intellect, agility, stamina, equipType);
					break;
				case ItemType.Weapon:
					int damage = (int)item["damage"].intValue;
					WeaponType weaponType = (WeaponType)Enum.Parse(typeof(WeaponType), item["weaponType"].stringValue);
					itemObj = new Item_Weapon(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, damage, weaponType);
					break;
				case ItemType.Material:
					itemObj = new Item_Material(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite);
					break;
			}
			itemList.Add(itemObj);
		}
	}



	public void ParseScriptableObject()
	{
		//todo
		// itemList = new List<Item>();
		// foreach (var item in test.dataList)
		// {
		// 	Debug.Log(item.name);
		// }



	}
	/// <summary>
	/// 通过ID得到道具
	/// </summary>
	/// <param name="id">物品ID</param>
	/// <returns></returns>
	public Item GetItemById(int id)
	{
		foreach (Item item in itemList)
		{
			if (item.ID == id)
			{
				return item;
			}
		}
		return null;
	}

	/// <summary>
	/// 捡起物品槽中指定数量的物品
	/// </summary>
	/// <param name="item">拿起的物品</param>
	/// <param name="amount">拿起的数量</param>
	public void PickupItem(Item item, int amount)
	{
		pickedItem.SetItem(item, amount);
		isPickedItem = true;
		pickedItem.Show();
		toolTipPanel.Hide();
	}

	/// <summary>
	/// 移除拿起的指定个数的物品
	/// </summary>
	/// <param name="amount">移除的数量</param>
	public void RemoveItem(int amount = 1)
	{
		pickedItem.ReduceAmount(amount);
		if (pickedItem.Amount <= 0)
		{
			isPickedItem = false;
			pickedItem.Hide();
		}
	}

	/// <summary>
	/// 显示提示面板
	/// </summary>
	/// <param name="content">提示内容</param>
	public void ShowToolTip(string content)
	{
		if (this.isPickedItem)
		{
			return;
		}
		isToolTipShow = true;
		toolTipPanel.Show(content);
		//Debug.Log("显示提示");
	}

	/// <summary>
	/// 隐藏提示面板
	/// </summary>
	public void HideToolTip()
	{
		isToolTipShow = false;
		toolTipPanel.Hide();
		//Debug.Log("隐藏提示");
	}

	// public void PutShowObject()
	// {
	// 	ResMgr.Instance.LoadAsync<GameObject>("Test", (showObj) =>
	// 	{
	// 		ShowObject = showObj;
	// 		ShowObject.transform.parent = GameObject.Find("ObjPos").transform;
	// 		ShowObject.transform.localPosition = Vector3.zero;
	// 	});
	// }

	/// <summary>
	/// 获取不被销毁的物品 
	/// </summary>
	/// <returns></returns>
	private GameObject[] getDontDestroyOnLoadGameObjects()
	{
		var allGameObjects = new List<GameObject>();
		allGameObjects.AddRange(FindObjectsOfType<GameObject>());
		//移除所有场景包含的对象
		for (var i = 0; i < SceneManager.sceneCount; i++)
		{
			var scene = SceneManager.GetSceneAt(i);
			var objs = scene.GetRootGameObjects();
			for (var j = 0; j < objs.Length; j++)
			{
				allGameObjects.Remove(objs[j]);
			}
		}
		//移除父级不为null的对象
		int k = allGameObjects.Count;
		while (--k >= 0)
		{
			if (allGameObjects[k].transform.parent != null)
			{
				allGameObjects.RemoveAt(k);
			}
		}
		return allGameObjects.ToArray();
	}

	//public void SaveInventory()
	//{

	//	//Chest.Instance.SaveInventory();
	//	//CharacterPanel.Instance.SaveInventory();
	//	//Forge.Instance.SaveInventory();
	//	PlayerPrefs.SetInt("CoinAmount", GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CoinAmount);
	//}

	public void LoadInventory()
	{

		//Chest.Instance.LoadInventory();
		//CharacterPanel.Instance.LoadInventory();
		//Forge.Instance.LoadInventory();
		//if (PlayerPrefs.HasKey("CoinAmount"))
		//{
		//	GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CoinAmount = PlayerPrefs.GetInt("CoinAmount");
		//}
	}
}
