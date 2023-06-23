//
// using System;
// using Cinemachine;
// using MyUtils;
// using UnityEngine;
// using Range = System.Range;
//
// namespace GamePlay
// {
//     [Serializable]
//     public class PlayerViewData
//     {
//         /// <summary>
//         /// 滚轮阻尼量
//         /// </summary>
//         [Tooltip("Damper for scrollwheel.")] [Range(0, 10)]
//         public float Damper = 5;
//
//
//         #region 摄像机控制相关
//
//         [Header("CameraController About")]
//         [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow\n虚拟相机的跟踪点")]
//         public GameObject CinemachineCameraTarget;
//
//         [Tooltip("How fast the character turns to face movement direction\n角色转向方向所需时间 ")] [Range(0.0f, 0.3f)]
//         public float RotationSmoothTime = 0.12f;
//
//         [Tooltip("How far in degrees can you move the camera up\n最大限制高度")]
//         public float TopClamp = 70.0f;
//
//         [Tooltip("How far in degrees can you move the camera down\n最小限制高度")]
//         public float BottomClamp = -30.0f;
//
//         [Tooltip(
//             "Additional degress to override the camera. Useful for fine tuning camera position when locked\n相机绕注视点旋转角度")]
//         public float CameraAngleOverride = 0.0f;
//
//         [Tooltip("Sensitivity of mouse pointer\n鼠标灵敏度")] [Range(0.0f, 2.0f)]
//         public float PointerSensitivity = 1.0f;
//
//         [Tooltip("For locking the camera position on all axis\n锁定相机")]
//         public bool LockCameraPosition = false;
//
//         
//         #endregion
//     }
// }