// using KooFrame;
// using UnityEngine;
// //using DG.Tweening;
//
// public class GamePanel : BasePanel
// {
//     public CanvasGroup canvasGroup;
//     protected override void Awake()
//     {
//         base.Awake();
//     }
//     private void Start()
//     {
//         if (canvasGroup == null)
//         {
//             canvasGroup = GetComponent<CanvasGroup>();
//         }
//     }
//     protected override void OnClick(string btnName)
//     {
//         switch (btnName)
//         {
//             case "SettingBtn":
//                 UIMgr.Instance.OpenPanel<SettingPanel>("SettingPanel", UIMgr.E_UI_Layer.Middle, (panel) =>
//                 {
//                     panel.transform.localPosition = new Vector3(0, -550, 0);
//                     canvasGroup.blocksRaycasts = false;  // 使面板不能与鼠标交互
//                 });
//                 break;
//         }
//     }
//     public void InitInfo()
//     {
//         Debug.Log("初始化数据");
//     }
//
//     public override void OpenMe()
//     {
//         base.OpenMe();
//     }
//
//     public override void CloseMe()
//     {
//         base.CloseMe();
//     }
//     public override void UseMe()
//     {
//         canvasGroup.blocksRaycasts = true;
//     }
//     public override void DontUseMe()
//     {
//         canvasGroup.blocksRaycasts = false;
//     }
// }
