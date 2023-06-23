using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using KooFrame; //引入dotween的命名空间

public class TaskPanel : BasePanel
{

    private CanvasGroup canvasGroup;
    private void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    //处理页面的关闭
    public override void HideMe()
    {
        Debug.Log("Exit");
        canvasGroup.blocksRaycasts = false;     //使面板不能与鼠标交互
        // canvasGroup.alpha = 0;               //设置透明度，使面板不可见   
        canvasGroup.DOFade(0, 0.5f);            //使用动画使面板不可见
    }

    public void OnClosePanel()
    {
        UIMgr.Instance.PopPanel();
    }

    public override void OpenMe()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        canvasGroup.blocksRaycasts = true;
        // canvasGroup.alpha = 1;   
        canvasGroup.DOFade(1, 0.5f);
    }
}
