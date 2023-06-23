using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using KooFrame; //导入命名空空间   枚举的转换需要该空间
using UnityEngine.UI;

public class MainMenuPanel : BasePanel
{

    private CanvasGroup canvasGroup;

    public Button TaskButton;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        TaskButton.onClick.AddListener(() => UIMgr.Instance.PushPanel(UIPanelType.TaskPanel));
    }
    public override void OnPause()
    {
        //当弹出新版面时，让该主菜单面板和鼠标不再交互
        canvasGroup.blocksRaycasts = false;
    }
    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }
    public void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIMgr.Instance.PushPanel(panelType);
    }
}
