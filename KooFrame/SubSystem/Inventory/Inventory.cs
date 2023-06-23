using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	protected Slot[] slotList;      //物品槽集合 让子类可以访问
	private float targetAlpha = 1;  //目标透明度
	private float smoothing = 4;    //渐变的速度
	private CanvasGroup canvasGroup;

	private void Awake()
	{
		slotList = GetComponentsInChildren<Slot>();
	}
	public virtual void Start()
	{

		canvasGroup = GetComponent<CanvasGroup>();
	}

	void Update()
	{
		if (canvasGroup.alpha != targetAlpha)
		{
			canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
			if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < .01f)
			{
				canvasGroup.alpha = targetAlpha;
			}
		}
	}

	//通过ID储存物品
	public bool StoreItem(int id)
	{
		Item item = InventoryManager.Instance.GetItemById(id);   //通过id拿到物品
		return StoreItem(item);
	}

	public bool StoreItem(Item item)
	{
		if (item == null)
		{
			Debug.LogWarning("要存储的物品的id不存在");
			return false;
		}
		if (item.Capacity == 1)                           //当物品的容量值为1
		{
			Slot slot = FindEmptySlot();
			if (slot == null)
			{
				Debug.LogWarning("没有空的物品槽");
			}
			else
			{
				slot.StoreItem(item);                    //把物品存储到空物品槽中 
			}
		}
		else
		{
			Slot slot = FindSameTypeSlot(item);
			if (slot != null)
			{
				slot.StoreItem(item);                   //存储叠加物品
			}
			else
			{
				Slot emptySlot = FindEmptySlot();
				if (emptySlot != null)
				{
					emptySlot.StoreItem(item);          //存储在空位置
				}
				else
				{
					Debug.LogWarning("没有空的物品槽");
					return false;
				}
			}
		}
		return true;
	}

	/// <summary>
	/// 用来找到一个空的物品槽
	/// </summary>
	/// <returns></returns>
	private Slot FindEmptySlot()
	{
		foreach (Slot slot in slotList)
		{
			if (slot.transform.childCount == 0)
			{
				return slot;
			}
		}
		return null;
	}
	/// <summary>
	/// 找到相同类型的格子
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	private Slot FindSameTypeSlot(Item item)
	{
		foreach (Slot slot in slotList)
		{
			if (slot.transform.childCount >= 1 && slot.GetItemId() == item.ID && slot.IsFilled() == false)   //如果slot的子物体数量大于1，且类型相同且没有满
			{
				return slot;
			}
		}
		return null;
	}

	//待优化 玩家在不透明度消失到最后的时候才可以丢弃物品？
	public void Show()
	{
		canvasGroup.blocksRaycasts = true;
		targetAlpha = 1;
	}
	public void Hide()
	{
		canvasGroup.blocksRaycasts = false;
		targetAlpha = 0;
	}
	public void DisplaySwitch()
	{
		if (targetAlpha == 0)
		{
			Show();
		}
		else
		{
			Hide();
		}
	}


	#region Save and Load
	public void SaveInventory()
	{
		StringBuilder sb = new StringBuilder();
		foreach (Slot slot in slotList)
		{
			if (slot.transform.childCount > 0)
			{
				ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
				sb.Append(itemUI.ItemObj.ID + "," + itemUI.Amount + "-");
			}
			else
			{
				sb.Append("0-");
			}
		}
		PlayerPrefs.SetString(this.gameObject.name, sb.ToString());
	}
	public void LoadInventory()
	{
		if (PlayerPrefs.HasKey(this.gameObject.name) == false) return;
		string str = PlayerPrefs.GetString(this.gameObject.name);
		//print(str);
		string[] itemArray = str.Split('-');
		for (int i = 0; i < itemArray.Length - 1; i++)
		{
			string itemStr = itemArray[i];
			if (itemStr != "0")
			{
				//print(itemStr);
				string[] temp = itemStr.Split(',');
				int id = int.Parse(temp[0]);
				Item item = InventoryManager.Instance.GetItemById(id);
				int amount = int.Parse(temp[1]);
				for (int j = 0; j < amount; j++)
				{
					slotList[i].StoreItem(item);
				}
			}
		}
	}
	#endregion
}
