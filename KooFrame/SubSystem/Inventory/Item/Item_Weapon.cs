using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器类型
/// </summary>
public enum WeaponType
{
    OffHand,    //副手
    MainHand    //主手
}
/// <summary>
/// 武器类
/// </summary>
public class Item_Weapon : Item
{
    public int Damage { get; set; }  //破坏力
    public WeaponType WeaponType { get; set; }

    public Item_Weapon(int id, string name, ItemType type, ItemQuality quality, string des, int capacity, int buyPrice, int sellPrice, string sprite,
        int damage, WeaponType wptype) : base(id, name, type, quality, des, capacity, buyPrice, sellPrice, sprite)
    {
        Damage = damage;
        WeaponType = wptype;
    }
}
