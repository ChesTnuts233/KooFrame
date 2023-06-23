using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using KooFrame; //引入dotween的命名空间

public class ItemMassagePanel : BasePanel
{

    private CanvasGroup canvasGroup;

    private void Start()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
    }
    //处理页面的关闭
    public override void HideMe()
    {
        canvasGroup.blocksRaycasts = false;  // 使面板不能与鼠标交互
        // canvasGroup.alpha = 0;   //设置透明度，使面板不可见   
        transform.DOScale(0, 0.5f).OnComplete(() => canvasGroup.alpha = 0);//使用动画效果 面板不可见
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
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f);
    }

    public void OnItemButtonClick()
    {
        UIMgr.Instance.PushPanel(UIPanelType.ItemMassagePanel);

    }
    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }

    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }
}
