using System.Collections;
using System.Collections.Generic;
using KooFrame;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StandardPanel : BasePanel
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {

    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitInfo()
    {
        Debug.Log("初始化数据");
    }


    protected override void OnClick(string btnName)
    {

    }


    public override void OpenMe()   //显示面板后要做的事情
    {
        Debug.Log("显示面板");
    }
    public override void CloseMe()
    {

    }

}
