#if ENABLE_INPUT_SYSTEM
using System;
using KooFrame.BaseSystem.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KooFrame
{
    public class InputManager : KooSingletonMono<InputManager>
    {
        #region 全局输入的数据

        [Header("鼠标输入")] public Vector2 Look;

        [Header("移动输入")] public Vector2 Move;
        
        public float Stop;

        [Header("Movement Settings")] public bool analogMovement;

        [Header("指针锁定")] public bool cursorLocked = true;


        #region 第三人称控制输入

        public Vector2 Look3rd;
        public Vector2 ScrollScale;

        #endregion

        #endregion

        public PlayerAction PlayerMap;

        private void Awake()
        {
            PlayerMap = InputMapInstances.playerCtrlMap;

            //第三人称注册
            PlayerMap.Normal.Enable();
           
            PlayerMap.Normal.Look.performed += OnLookCtrl;
            PlayerMap.Normal.Look.canceled += OnLookCtrl;
            PlayerMap.Normal.ScrollScale.performed += OnScrollScaleCtrl;
            PlayerMap.Normal.ScrollScale.canceled += OnScrollScaleCtrl;

            // //第一人称注册
            // _playerMap.Player1stCtrl.Look.performed += OnLookCtrl;
            // _playerMap.Player1stCtrl.Look.canceled += OnLookCtrl;

        }
        
        private void OnLookCtrl(InputAction.CallbackContext context)
        {
            Look3rd = context.ReadValue<Vector2>();
        }

        private void OnScrollScaleCtrl(InputAction.CallbackContext context)
        {
            ScrollScale = context.ReadValue<Vector2>();
        }
        

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
        
    }
}
#endif