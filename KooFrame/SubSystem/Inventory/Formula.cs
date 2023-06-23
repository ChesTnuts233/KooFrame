using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formula
{
    public int Item1ID { get; private set; }
    public int Item1Amount { get; private set; }
    public int Item2ID { get; private set; }
    public int Item2Amount { get; private set; }

    public int ResID { get; private set; }          //锻造结果的物品 

    private List<int> needIdList = new List<int>(); //所需要的物品的id的集合

    public List<int> NeedIdList
    {
        get
        {
            return needIdList;
        }
    }
    public Formula(int item1ID, int item1Amount, int item2ID, int item2Amount, int resID)
    {
        this.Item1ID = item1ID;
        this.Item1Amount = item1Amount;
        this.Item2ID = item2ID;
        this.Item2Amount = item2Amount;
        this.ResID = resID;
        //将Item1、2的数量全部加入配方类中的所需物品集合中
        for (int i = 0; i < Item1Amount; i++)
        {
            needIdList.Add(Item1ID);
        }
        for (int i = 0; i < Item2Amount; i++)
        {
            needIdList.Add(Item2ID);
        }
    }
    /// <summary>
    /// 匹配算法
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    public bool Match(List<int> idList)
    {
        List<int> tempIDList = new List<int>(idList);   //新建临时集合存储目前尝试合成的物品
        foreach (int id in needIdList)
        {
            bool isSuccess = tempIDList.Remove(id);     //遍历集合 尝试移除集合中的所需物品 能移除返回true 反之返回false
            if (isSuccess == false)
            {
                return false;
            }
        }
        return true;
    }
}