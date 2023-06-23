#if ENABLE_INPUT_SYSTEM
using System;
using Cinemachine;
using GamePlay;
using MyUtils;
using SokoBan;
using UnityEngine;
using Range = MyUtils.Range;

namespace KooFrame.Views
{
    public class ThirdViewCtrl : KooMonoBaseView
    {
        [SerializeField] private PlayerViewData ViewData;
        private PlayerInputAndCtrl _input;

        #region Cinemachine

        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;
        private const float _threshold = 0.01f; //相机控制阈值

        private float cameraDistance = 6;
        [SerializeField] private CinemachineVirtualCamera virtualCamera3rd;
        private Cinemachine3rdPersonFollow cmv3rd;

        [Tooltip("Range limit of distance.")] private Range distanceRange = new Range(1, 10);

        [Tooltip("Settings of mouse button, pointer and scrollwheel.")]
        private MouseSettings mouseSettings = new MouseSettings(1, 10, 10);

        #endregion

        //判断当前是否为此操作方案
        private bool IsCurrentDeviceMouse
        {
            get
            {
                return true;
                // return _playerInput.currentControlScheme == "KeyboardAndMouse";
            }
        }


        private void Awake()
        {
            _cinemachineTargetYaw = ViewData.CinemachineCameraTarget.transform.rotation.eulerAngles.y;
            _input = GameObject.Find("Player").GetComponent<PlayerInputAndCtrl>();
            InitCinemachine3rd();
        }

        

        private void InitCinemachine3rd()
        {
            //初始化控制人物的Cinemachine3rd
            //Debug.Log(_virtualCamera);
            cmv3rd = _virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            cmv3rd.ShoulderOffset = new Vector3(1.0f, 0.18f, 0);
            cmv3rd.Damping = new Vector3(0.1f, 0.25f, 0.3f);
        }

        protected override void ViewCtrl()
        {
            ScrollController();
            CameraRotationController();
        }

        #region 摄像机控制

        /// <summary>
        /// 鼠标滚轮控制
        /// </summary>
        private void ScrollController()
        {
            //Mouse scrollwheel.
            cameraDistance -= _input.ScrollScale.y * mouseSettings.wheelSensitivity;
            cameraDistance = Mathf.Clamp(cameraDistance, distanceRange.min, distanceRange.max);
            cmv3rd.CameraDistance = Mathf.Lerp(cmv3rd.CameraDistance, cameraDistance, ViewData.Damper * Time.deltaTime);
        }

        /// <summary>
        /// 摄像机控制
        /// </summary>
        private void CameraRotationController()
        {
            // if there is an input and camera position is not fixed //当看向方向的根号模长大于阈值的时候
            if (_input.Look3rd.sqrMagnitude >= _threshold && !ViewData.LockCameraPosition)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? ViewData.PointerSensitivity : Time.deltaTime;

                _cinemachineTargetYaw += _input.Look3rd.x * 1 /*deltaTimeMultiplier*/;
                _cinemachineTargetPitch += _input.Look3rd.y * 1 /*deltaTimeMultiplier*/;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, ViewData.BottomClamp, ViewData.TopClamp);

            // Cinemachine will follow this target
            ViewData.CinemachineCameraTarget.transform.rotation = Quaternion.Euler(
                _cinemachineTargetPitch + ViewData.CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        #endregion
    }
}

#endif