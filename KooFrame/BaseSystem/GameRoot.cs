using KooFrame.BaseSystem.Singleton;
using KooFrame.KooManagers.UIManager;
using KooFrame.Managers;
using UnityEngine;

namespace KooFrame
{
    /// <summary>
    /// GameRoot是一个单例Mono类
    /// </summary>
    public class GameRoot : KooPortalManager
    {
        public GameObject KooCanvas;


        public bool IsLaunchStartPanel = false;

        public bool IsLaunchInventoryPanel = false;

// #if UNITY_EDITOR //此为宏定义标签 UNITY_EDITOR表示这段代码只在Editor模式下执行 发布后剔除
//         [MenuItem("CONTEXT/UIOpen/LaunchLoginPanel")]
//         public static void NewContext2(MenuCommand command)
//         {
//             UIOpen script = (command.context as UIOpen);
//             if (script.IsLaunchStartPanel == false)
//             {
//                 script.IsLaunchStartPanel = true;
//                 Debug.Log("LoginPanel Launch");
//             }
//             else
//             {
//                 script.IsLaunchStartPanel = false;
//                 Debug.Log("LoginPanel Close");
//             }
//         }
// #endif

        protected new void Awake()
        {
            //base.Awake();
            //让GameRoot过场不销毁
            DontDestroyOnLoad(this);
            //获取到GameRoot中的组件
            KooCanvas = GameObject.Find("KooCanvas");
        }

        private void Start()
        {
            MusicMgr.Instance.PlayBGMUsic("BG1");
            MusicMgr.Instance.ChangeBUMusicValue(0.3f);
            if (IsLaunchInventoryPanel)
            {
                UIMgr.Instance.OpenPanel<InventoryPanel>(
                    "InventoryPanel",
                    UIMgr.E_UI_Layer.Bottom,
                    (panel) =>
                    {
                        panel.transform.position = new Vector3(0, -400, 0);
                        panel.HideMe();
                    },
                    false);
            }

            if (IsLaunchStartPanel)
            {
                //UIMgr.Instance.OpenPanel<StartPanel>("StartPanel", UIMgr.E_UI_Layer.Bottom);

                // var test = new KooUIManager();
            }

#if ENABLE_INPUT_SYSTEM
            // CinemachineManager cinemachineManager = GameObject.Find("CameraManager").GetComponent<CinemachineManager>();
            //设置当前初始摄像机为看向人物的第三人称相机
            //ViewManager.Instance.OpenCMCameraBySetActive("3rdCameraCtrl");
#endif

            //EventCenter.Instance.EventTrigger<bool>("Player第三人称控制", true);
        }


        protected override void LaunchInDevelopingMode()
        {
            //throw new System.NotImplementedException();
        }
        
        protected override void LaunchInTestingMode()
        {
            //throw new System.NotImplementedException();
        }
        
        protected override void LaunchInProductionMode()
        {
            //throw new System.NotImplementedException();
        }
    }
}