using Defective.JSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : Inventory
{
    #region 单例模式
    private static Forge instance;
    public static Forge Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("ForgePanel").GetComponent<Forge>();
            }
            return instance;
        }
    }
    #endregion

    private List<Formula> formulaList;                                      //配方集合

    public override void Start()
    {
        base.Start();
        //ParseFormulaJson();
    }

    /// <summary>
    /// 解析配方信息
    /// </summary>
    void ParseFormulaJson()
    {
        formulaList = new List<Formula>();
        TextAsset formulasText = Resources.Load<TextAsset>("Formulas");     //文本在Unity中是TextAsset类型 
        string formulasJson = formulasText.text;                            //配方信息的Json数据
        JSONObject jo = new JSONObject(formulasJson);
        foreach (JSONObject temp in jo.list)
        {
            int item1ID = (int)temp["Item1ID"].intValue;
            int item1Amount = (int)temp["Item1Amount"].intValue;
            int item2ID = (int)temp["Item2ID"].intValue;
            int item2Amount = (int)temp["Item2Amount"].intValue;
            int resID = (int)temp["ResID"].intValue;
            Formula formula = new Formula(item1ID, item1Amount, item2ID, item2Amount, resID);
            formulaList.Add(formula);                                       //加入配方集合中
        }
    }

    public void ForgeItem()
    {
        // 得到当前有哪些材料
        // 判断满足哪一个秘籍的要求

        List<int> haveMaterialIDList = new List<int>();                 //存储当前拥有的材料的id
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                ItemUI currentItemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                for (int i = 0; i < currentItemUI.Amount; i++)
                {
                    haveMaterialIDList.Add(currentItemUI.ItemObj.ID);   //这个格子里面有多少个物品 就存储多少个id
                }
            }
        }

        Formula matchedFormula = null;                                      //要去测试匹配的配方
        foreach (Formula formula in formulaList)
        {
            bool isMatch = formula.Match(haveMaterialIDList);
            if (isMatch)                                                    //匹配成功则跳出
            {
                matchedFormula = formula; break;
            }
        }
        if (matchedFormula != null)                                         //如果匹配出来的配方不为空
        {
            Knapsack.Instance.StoreItem(matchedFormula.ResID);              //存入合成物品
            foreach (int id in matchedFormula.NeedIdList)                   //去掉消耗的材料
            {
                foreach (Slot slot in slotList)
                {
                    if (slot.transform.childCount > 0)                      //合成槽内有物品
                    {
                        ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                        if (itemUI.ItemObj.ID == id && itemUI.Amount > 0)   //遍历配方与槽内的ID，相同就减去物品
                        {
                            itemUI.ReduceAmount();
                            if (itemUI.Amount <= 0)
                            {
                                DestroyImmediate(itemUI.gameObject);
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}