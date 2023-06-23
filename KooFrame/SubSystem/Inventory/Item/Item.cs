using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品类型
/// </summary>
public enum ItemType
{
    Consumable,
    Equipment,
    Weapon,
    Material
}
/// <summary>
/// 品质
/// </summary>
public enum ItemQuality
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Artifact
}

/// <summary>
/// 物品基类
/// </summary>
public class Item : ScriptableObject
{
    public int ID { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public ItemQuality Quality { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
    public string Sprite { get; set; }

    /// <summary>
    /// 物品基类
    /// </summary>

    public Item()
    {
        this.ID = -1;
    }

    public Item(int id, string name, ItemType type, ItemQuality quality, string description, int capacity, int buyPrice, int sellPrice, string sprite)
    {
        this.ID = id;
        this.Name = name;
        this.Type = type;
        this.Quality = quality;
        this.Description = description;
        this.Capacity = capacity;
        this.BuyPrice = buyPrice;
        this.SellPrice = sellPrice;
        this.Sprite = sprite;
    }
    /// <summary>
    /// 返回物品提示内容
    /// </summary>
    /// <returns></returns>
    public virtual string GetToolTipText()
    {
        string color = "";
        switch (Quality)
        {
            case ItemQuality.Common:
                color = "white";
                break;
            case ItemQuality.Uncommon:
                color = "lime";
                break;
            case ItemQuality.Rare:
                color = "navy";
                break;
            case ItemQuality.Epic:
                color = "magenta";
                break;
            case ItemQuality.Legendary:
                color = "orange";
                break;
            case ItemQuality.Artifact:
                color = "red";
                break;
            default:
                break;
        }
        string text = string.Format("<color={0}>{1}</color>\n购买价格,{2}出售价格:{3}\n{4}", color, Name, BuyPrice, SellPrice, Description);

        return text;
    }
}


