using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Item;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
	public GameObject itemPrefab;
	/// <summary>
	/// 把item放在自身的下面
	/// 如果有item  amount++
	/// 如没有 根据itemPrefab去实例化一个item 放在下面
	/// </summary>
	/// <param name="item">存放的物品</param>
	public void StoreItem(Item item)
	{
		if (transform.childCount == 0)                                      //如果子物体数量等于零
		{
			GameObject itemGameObject = Instantiate(itemPrefab);            //获得实例对象
			itemGameObject.transform.SetParent(this.transform);             //设置位置坐标
			itemGameObject.transform.localPosition = Vector3.zero;          //本地坐标设置为零
			itemGameObject.transform.localScale = Vector3.one;
			itemGameObject.GetComponent<ItemUI>().SetItem(item);            //道具物品添加ItemUI组件
		}
		else
		{
			transform.GetChild(0).GetComponent<ItemUI>().AddAmount();       //不为零就添加物品
		}
	}
	/// <summary>
	/// 得到当前槽位的物品类型
	/// </summary>
	/// <returns></returns>
	public ItemType GetItemType()
	{
		return transform.GetChild(0).GetComponent<ItemUI>().ItemObj.Type;   //获取当前物品槽存储的物品类型
	}

	/// <summary>
	/// 得到物品的id
	/// </summary>
	/// <returns></returns>
	public int GetItemId()
	{
		return transform.GetChild(0).GetComponent<ItemUI>().ItemObj.ID;
	}

	/// <summary>
	/// 判断槽位是否已满
	/// </summary>
	/// <returns></returns>
	public bool IsFilled()
	{
		ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
		return itemUI.Amount >= itemUI.ItemObj.Capacity;                    //当前的数量大于等于容量
	}

	private void Start()
	{
		InventoryManager.Instance.toolTipPanel = GameObject.FindObjectOfType<ToolTipPanel>().GetComponent<ToolTipPanel>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (transform.childCount > 0)
		{
			string toolTipText = transform.GetChild(0).GetComponent<ItemUI>().ItemObj.GetToolTipText();
			InventoryManager.Instance.ShowToolTip(toolTipText);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (transform.childCount > 0)
		{
			InventoryManager.Instance.HideToolTip();
		}
	}

	public virtual void OnPointerDown(PointerEventData eventData)
	{
		////快捷穿脱
		//if (eventData.button == PointerEventData.InputButton.Right) //当按下右键的时候
		//{   //当未拾取物品并且槽内有东西的时候
		//	if (InventoryManager.Instance.IsPickedItem == false && transform.childCount > 0)
		//	{
		//		ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
		//		if (currentItemUI.ItemObj is Item_Equipment || currentItemUI.ItemObj is Item_Weapon)
		//		{
		//			currentItemUI.ReduceAmount(1);                  //减少数量
		//			Item currentItem = currentItemUI.ItemObj;       //获取这个装备
		//			if (currentItemUI.Amount <= 0)
		//			{
		//				DestroyImmediate(currentItemUI.gameObject);  //销毁物品
		//				InventoryManager.Instance.HideToolTip();     //隐藏提示面板
		//			}
		//			CharacterPanel.Instance.PutOn(currentItem);
		//		}
		//	}

		//}

		#region 逻辑说明
		// 自身是空 1,IsPickedItem ==true  pickedItem放在这个位置
		//                       按下左ctrl      放置当前鼠标上的物品的一个
		//                       没有按下左ctrl   放置当前鼠标上的物品的所有
		//         2,IsPickedItem==false  不做任何处理
		// 自身不是空 
		//         1,IsPickedItem==true
		//                自身的id==pickedItem.id   pickedItem放置到当前物品
		//                       按下左ctrl      放置当前鼠标上的物品的一个
		//                       没有按下左ctrl   放置当前鼠标上的物品的所有
		//                                          可以完全放下
		//                                          只能放下其中一部分
		//                自身的id!=pickedItem.id   pickedItem跟当前物品交换          
		//         2,IsPickedItem==false          
		//                       ctrl按下 取得当前物品槽中物品的一半
		//                       ctrl没有按下 把当前物品槽里面的物品放到鼠标上
		#endregion

		if (transform.childCount > 0)  //自身不空
		{
			ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();      //当前拿着的物品
			if (InventoryManager.Instance.IsPickedItem == false)                    //当前没有拿任何物品
			{
				if (Input.GetKey(KeyCode.LeftControl))                              //是否按着左ctrl
				{
					int amountPicked = (currentItem.Amount + 1) / 2;                //保证奇数的时候取值多取一个
					InventoryManager.Instance.PickupItem(currentItem.ItemObj, amountPicked);
					int amountRemained = currentItem.Amount - amountPicked;         //槽内剩余个数
					if (amountRemained <= 0)
					{
						Destroy(currentItem.gameObject);                            //当剩余数量小于等于零时 销毁物品
					}
					else
					{
						currentItem.SetAmount(amountRemained);                      //否则显示剩余数量
					}
				}
				else                                                                //不按左ctrl时，直接取物
				{
					InventoryManager.Instance.PickupItem(currentItem.ItemObj, currentItem.Amount);
					Destroy(currentItem.gameObject);
				}
			}
			else  //IsPickedItem==true                                                          //鼠标上有物品
			{
				if (currentItem.ItemObj.ID == InventoryManager.Instance.PickedItem.ItemObj.ID)  //槽内物品等于捡起来的物品ID
				{
					if (Input.GetKey(KeyCode.LeftControl))    //按着左ctrl   一个一个放
					{
						if (currentItem.ItemObj.Capacity > currentItem.Amount)                  //当前物品槽还有容量
						{
							currentItem.AddAmount();
							InventoryManager.Instance.RemoveItem();
						}
						else
						{
							return;
						}
					}
					else                                      //没按左ctrl   直接放                           
					{
						if (currentItem.ItemObj.Capacity > currentItem.Amount)                      //槽内物品数量小于容量时
						{
							int spaceRemain = currentItem.ItemObj.Capacity - currentItem.Amount;    //当前物品槽内剩余空间
							if (spaceRemain >= InventoryManager.Instance.PickedItem.Amount)         //当剩余空间大于鼠标上的物品数量时
							{
								currentItem.SetAmount(currentItem.Amount + InventoryManager.Instance.PickedItem.Amount);
								InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
							}
							else
							{
								currentItem.SetAmount(currentItem.Amount + spaceRemain);
								InventoryManager.Instance.RemoveItem(spaceRemain);
							}
						}
						else
						{
							return;     //槽内已满
						}
					}
				}
				else                                                                                //槽内物品不等于捡起来的物品ID         
				{
					Item item = currentItem.ItemObj;                                                //当前槽内物品
					int amount = currentItem.Amount;                                                //当前槽内数量
					currentItem.SetItem(InventoryManager.Instance.PickedItem.ItemObj, InventoryManager.Instance.PickedItem.Amount);
					InventoryManager.Instance.PickedItem.SetItem(item, amount);                     //鼠标上的物品换为槽内物品
				}
			}
		}
		else   //自身为空
		{
			if (InventoryManager.Instance.IsPickedItem == true)                     //鼠标上有东西
			{
				if (Input.GetKey(KeyCode.LeftControl))
				{
					this.StoreItem(InventoryManager.Instance.PickedItem.ItemObj);
					InventoryManager.Instance.RemoveItem();
				}
				else
				{
					for (int i = 0; i < InventoryManager.Instance.PickedItem.Amount; i++)
					{
						this.StoreItem(InventoryManager.Instance.PickedItem.ItemObj);
					}
					InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
				}
			}
			else
			{
				return;
			}
		}
	}
}