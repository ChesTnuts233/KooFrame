using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //引入dotween的命名空间
using KooFrame;

public class BagPanel : BasePanel
{

    private CanvasGroup canvasGroup;

    private void Start()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
    }
    //处理页面的关闭
    public override void HideMe()
    {
        // canvasGroup.alpha = 0;               //设置透明度，使面板不可见   
        canvasGroup.blocksRaycasts = false;     // 使面板不能与鼠标交互
        transform.DOLocalMoveX(600, 0.5f).OnComplete(() => canvasGroup.alpha = 0);
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

        Vector3 temp = transform.localPosition;
        temp.x = 600;
        transform.localPosition = temp;        //设置面板出现前的位置  
        transform.DOLocalMoveX(0, 0.5f);
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
