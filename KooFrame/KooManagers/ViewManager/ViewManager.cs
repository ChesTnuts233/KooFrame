#if ENABLE_INPUT_SYSTEM


using Cinemachine;
using System.Collections.Generic;
using KooFrame;
using KooFrame.BaseSystem.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KooFrame
{
    /// <summary>
    /// 视角管理
    /// 视角管理模块是基于InputSystem和Cinemachine系统的 用于在特定的时候处理视角和操作模式的切换
    /// </summary>
    public class ViewManager : KooSingletonMono<ViewManager>
    {
        public GameObject MainCamera;
        public CinemachineBrain CMBrain;

        //虚拟相机配置的字典
        public Dictionary<string, GameObject> CMcamerasDic = new Dictionary<string, GameObject>();

        public GameObject followCine;
        public GameObject otherCine;

        // //操作方案的字典
        // public Dictionary<string, KooBaseInput> InputsDic = new Dictionary<string, KooBaseInput>();

        protected override void Awake()
        {
            base.Awake();
            if (Camera.main != null) MainCamera = Camera.main.gameObject;
            CMBrain = MainCamera.GetComponent<CinemachineBrain>();
            //初始化相机
            foreach (Transform child in this.transform)
            {
                var go = child.gameObject;
                CMcamerasDic.Add(go.name, go);
            }
            
            // //初始化操作方案
            // foreach (var input in InputsDic)
            // {
            //     
            // }
        }

        private void Start()
        {
            Debug.Log(CMcamerasDic.Keys.Count);
        }

        // private void Update()
        // {
        //     //Change3rdTo1st();
        //     //OrderChangeCamera();
        // }


        // private void Change3rdTo1st()
        // {
        //     //从第三人称转到第一人称
        //     if (CMcameras["3rdCameraCtrl"].GetComponent<CinemachineVirtualCamera>()
        //             .GetCinemachineComponent<Cinemachine3rdPersonFollow>()
        //             .CameraDistance <= 1
        //         && CMcameras["1stCameraCtrl"].GetComponent<CameraCtrl_1st>().Input.currentActionMap.name ==
        //         "Camera3rdCtrl")
        //     {
        //         Changeto1stCtrl();
        //     }
        // }

        /// <summary>
        /// 启动虚拟相机
        /// </summary>
        /// <param name="CameraName">启动的虚拟相机名字</param>
        public void OpenCMCameraBySetActive(string CameraName)
        {
            CMcamerasDic[CameraName].SetActive(false);
            CMcamerasDic[CameraName].SetActive(true);
        }
        
        
        
        private void SwitchCamera()
        {
        }
    }
}

#endif