using UnityEngine;
using UnityEngine.UI;
using FrameTest;
using PlayerTest = FrameTest.Player;

public class CharacterPanel : Inventory
{
    #region 单例模式
    private static CharacterPanel instance;
    public static CharacterPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("CharacterPanel").GetComponent<CharacterPanel>();
            }
            return instance;
        }
    }
    #endregion

    private Text propertyText;  //属性文字

    private PlayerTest player;

    public override void Start()
    {
        base.Start();
        propertyText = transform.Find("PropertyPanel/Text").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTest>();
        UpdatePropertyText();
        Hide();
    }

    /// <summary>
    /// 穿上物品
    /// </summary>
    /// <param name="item"></param>
    public void PutOn(Item item)
    {
        Item exitItem = null;
        foreach (Slot slot in slotList)
        {
            EquipmentSlot equipmentSlot = (EquipmentSlot)slot;
            if (equipmentSlot.IsRightItem(item))        //如果是装备
            {
                if (equipmentSlot.transform.childCount > 0)     //槽内有东西
                {
                    ItemUI currentItemUI = equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>();
                    exitItem = currentItemUI.ItemObj;           //拿出物品存入临时变量
                    currentItemUI.SetItem(item, 1);             //穿上装备
                }
                else                                            //槽内无东西
                {
                    equipmentSlot.StoreItem(item);              //直接穿上装备
                }
                break;
            }
        }
        if (exitItem != null)
        {
            Knapsack.Instance.StoreItem(exitItem);              //被替换的物品放回背包中
        }
        UpdatePropertyText();
    }

    public void PutOff(Item item)
    {
        Knapsack.Instance.StoreItem(item);                     //直接存入背包
        UpdatePropertyText();
    }
    /// <summary>
    /// 更新属性文字
    /// </summary>
    private void UpdatePropertyText()
    {
        int strength = 0, intellect = 0, agility = 0, stamina = 0, damage = 0;
        foreach (EquipmentSlot slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                Item item = slot.transform.GetChild(0).GetComponent<ItemUI>().ItemObj;
                if (item is Item_Equipment)
                {
                    Item_Equipment e = (Item_Equipment)item;
                    strength += e.Strength;
                    intellect += e.Intellect;
                    agility += e.Agility;
                    stamina += e.Stamina;
                }
                else if (item is Item_Weapon)
                {
                    damage += ((Item_Weapon)item).Damage;
                }
            }
        }
        strength += player.BasicStrength;
        intellect += player.BasicIntellect;
        agility += player.BasicAgility;
        stamina += player.BasicStamina;
        damage += player.BasicDamage;
        string text = string.Format("力量：{0}\n智力：{1}\n敏捷：{2}\n体力：{3}\n攻击力：{4} ", strength, intellect, agility, stamina, damage);
        propertyText.text = text;
    }
}
