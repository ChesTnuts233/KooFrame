// #if ENABLE_INPUT_SYSTEM
//
//
// using Cinemachine;
// using UnityEngine;
// using MyUtils;
// using UnityEngine.InputSystem;
//
// namespace KooFrame
// {
//     public class CameraCtrl_3rd : MonoBehaviour
//     {
//         
//         /// <summary>
//         /// 滚轮阻尼量
//         /// </summary>
//         [Tooltip("Damper for scrollwheel.")] [Range(0, 10)]
//         public float damper = 5;
//
//         public static bool is3rdCtrl = true;
//
//
//         public PlayerInput Input;
//         public CameraInput CameraInput;
//         public Transform CameraPlayer;
//         CameraCtrl CameraCtrl;
//         public CinemachineManager CMmanager;
//
//         private bool IsCurrentDeviceMouse
//         {
//             get
//             {
// #if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
// 			return Input.currentControlScheme == "Camera_KeyboardAndMouse";
// #else
//                 return false;
// #endif
//             }
//         }
//
//         // private void Awake()
//         // {
//         //     //Cinemachine3rd
//         //     virtualCamera3rd = this.GetComponent<CinemachineVirtualCamera>();
//         //     cmv3rd = virtualCamera3rd.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
//         //     cmv3rd.ShoulderOffset = new Vector3(1.0f, 0.18f, 0);
//         //     cmv3rd.Damping = new Vector3(0.1f, 0.25f, 0.3f);
//         // }
//
//         private void OnEnable()
//         {
//             CameraInput.SetCursorState(true);
//             Input.SwitchCurrentActionMap("Camera3rdCtrl");
//             //CinemachineCameraTarget = GameObject.Find("PlayerCameraRoot");
//             Debug.Log(Input.currentActionMap.name);
//         }
//
//
//         private void LateUpdate()
//         {
//             ScrollController();
//             CameraRotationController();
//         }
//
//         #region 摄像机鼠标控制
//
//         /// <summary>
//         /// 鼠标滚轮控制
//         /// </summary>
//         private void ScrollController()
//         {
//             //Mouse scrollwheel.
//             cameraDistance -= CameraInput.ScrollScale.y * mouseSettings.wheelSensitivity;
//             cameraDistance = Mathf.Clamp(cameraDistance, distanceRange.min, distanceRange.max);
//             cmv3rd.CameraDistance = Mathf.Lerp(cmv3rd.CameraDistance, cameraDistance, damper * Time.deltaTime);
//         }
//
//         /// <summary>
//         /// 摄像机控制
//         /// </summary>
//         private void CameraRotationController()
//         {
//             // if there is an input and camera position is not fixed //当看向方向的根号模长大于阈值的时候
//             if (CameraInput.Look3rd.sqrMagnitude >= _threshold && !LockCameraPosition)
//             {
//                 //Don't multiply mouse input by Time.deltaTime;
//                 float deltaTimeMultiplier = IsCurrentDeviceMouse ? PointerSensitivity : Time.deltaTime;
//
//                 _cinemachineTargetYaw += CameraInput.Look3rd.x * 1 /*deltaTimeMultiplier*/;
//                 _cinemachineTargetPitch += CameraInput.Look3rd.y * 1 /*deltaTimeMultiplier*/;
//             }
//
//             // clamp our rotations so our values are limited 360 degrees
//             _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
//             _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);
//
//             // Cinemachine will follow this target
//             CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
//                 _cinemachineTargetYaw, 0.0f);
//         }
//
//         private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
//         {
//             if (lfAngle < -360f) lfAngle += 360f;
//             if (lfAngle > 360f) lfAngle -= 360f;
//             return Mathf.Clamp(lfAngle, lfMin, lfMax);
//         }
//
//         #endregion
//     }
//
//     public class CinemachineManager
//     {
//     }
// #endif
// }