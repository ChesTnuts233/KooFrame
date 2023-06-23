using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{
    public EquipmentType equipType;
    public WeaponType weaponType;

    //判断item是否适合放在这个位置
    public bool IsRightItem(Item item)
    {                               /*IDE推荐 模式匹配 ↓ 两种写法等效*/
        if ((item is Item_Equipment equipment && equipment.EquipType == this.equipType) ||
            (item is Item_Weapon && ((Item_Weapon)item).WeaponType == this.weaponType))
        {
            return true;
        }
        return false;
    }



    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (InventoryManager.Instance.IsPickedItem == false && transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();    //得到当前槽内物品
                Item itemTemp = currentItemUI.ItemObj;
                DestroyImmediate(currentItemUI.gameObject);                             //脱掉放入背包
                transform.parent.SendMessage("PutOff", itemTemp);
                InventoryManager.Instance.HideToolTip();
            }
        }
        if (eventData.button != PointerEventData.InputButton.Left) return;              //不是左键时返回

        bool isUpdateProperty = false;                                                  //是否更新属性
        if (InventoryManager.Instance.IsPickedItem == true)                             //手上有东西的情况
        {
            ItemUI pickedItem = InventoryManager.Instance.PickedItem;
            if (transform.childCount > 0)                                               //当前装备槽内有装备
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();    //当前装备槽里面的物品
                if (IsRightItem(pickedItem.ItemObj))
                {
                    InventoryManager.Instance.PickedItem.Exchange(currentItemUI);       //交换物品
                    isUpdateProperty = true;
                }
            }
            else                                                                        //当前装备槽内没装备
            {
                if (IsRightItem(pickedItem.ItemObj))
                {
                    this.StoreItem(InventoryManager.Instance.PickedItem.ItemObj);
                    InventoryManager.Instance.RemoveItem(1);
                    isUpdateProperty = true;

                }
            }
        }
        else
        {
            //手上没东西的情况
            if (transform.childCount > 0)                                                //槽内有东西
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                InventoryManager.Instance.PickupItem(currentItemUI.ItemObj, currentItemUI.Amount); //拿起装备
                Destroy(currentItemUI.gameObject);
                isUpdateProperty = true;

            }
        }
        if (isUpdateProperty)
        {
            transform.parent.SendMessage("UpdatePropertyText");
        }
    }

}