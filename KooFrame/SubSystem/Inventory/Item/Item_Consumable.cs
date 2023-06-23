using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Consumable : Item
{
    public int HP { get; set; }    //血量
    public int MP { get; set; }    //魔量

    public Item_Consumable(int id, string name, ItemType type, ItemQuality quality, string des, int capacity, int buyPrice, int sellPrice, string sprite, int hp, int mp)
        : base(id, name, type, quality, des, capacity, buyPrice, sellPrice, sprite)
    {
        HP = hp;
        MP = mp;
    }

    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();
        string newText = string.Format("{0} \n\n<color=blue>加血: {1}\n加蓝： {2}</color>", text, HP, MP);
        return newText;
    }
}
