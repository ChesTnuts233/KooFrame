#if ENABLE_INPUT_SYSTEM
using System;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KooFrame
{
    /// <summary>
    /// 基础视角类，类中有基本操作方案，摄像机控制
    /// </summary>
    public abstract class KooMonoBaseView : MonoBehaviour
    {
        [SerializeField] protected IInputActionCollection2 _inputAction;
        [SerializeField] protected CinemachineVirtualCamera _virtualCamera;
        [SerializeField] protected KooMonoBaseInput _input;                                      //输入控制器

        [SerializeField] [CanBeNull] protected GameObject _ctrlGameObject = null;                //视角控制的对象

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            ViewCtrl();
        }

        protected abstract void ViewCtrl();
    }
}
#endif