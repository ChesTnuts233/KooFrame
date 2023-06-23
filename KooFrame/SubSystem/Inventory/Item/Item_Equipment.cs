using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 装备类型
/// </summary>
public enum EquipmentType
{
    Head,
    Neck,
    Ring,
    Leg,
    Chest,
    Bracer,  //护腕
    Boots,
    Trinket, //装饰品
    Shoulder,
    Belt,
    OffHand

}
public class Item_Equipment : Item
{
    public int Strength { get; set; }               //力量
    public int Intellect { get; set; }              //智力
    public int Agility { get; set; }                //敏捷
    public int Stamina { get; set; }                //体力
    public EquipmentType EquipType { get; set; }    //装备类型

    public Item_Equipment(int id, string name, ItemType type, ItemQuality quality, string description, int capacity, int buyPrice, int sellPrice, string sprite,
        int strength, int intellect, int agility, int stamina, EquipmentType equipType)
        : base(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite)
    {
        Strength = strength;
        Intellect = intellect;
        Agility = agility;
        Stamina = stamina;
        EquipType = equipType;
    }

}
