using System.Collections;
using System.Collections.Generic;
using KooFrame;
using UnityEngine;
//using DG.Tweening;

public class SettingPanel : BasePanel
{
    protected override void Awake()
    {
        base.Awake();
    }
    public void InitInfo()
    {
        Debug.Log("初始化数据");
    }
    public override void OpenMe()
    {
        //this.transform.DOLocalMove(Vector3.zero, 2).OnComplete(() => { Time.timeScale = 0; });
    }

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "ExitBtn":
                {
                    UIMgr.Instance.ClosePanel("SettingPanel");
                    Time.timeScale = 1;
                    UIMgr.Instance.CanvasGroupControler("GamePanel", true);
                }
                break;
        }
    }

    public override void CloseMe()
    {
        base.CloseMe();
    }
}
