using System;
using KooFrame.BaseSystem.Singleton;
using UnityEngine;

namespace KooFrame.Managers
{
    /// <summary>
    /// 项目工程状态
    /// </summary>
    public enum EnvironmentMode
    {
        /// <summary>
        /// 开发阶段
        /// </summary>
        Developing,
        /// <summary>
        /// 测试阶段
        /// </summary>
        Testing, 
        /// <summary>
        /// 产品阶段
        /// </summary>
        Production, 
    }

    /// <summary>
    /// 游戏门户 这里判断了工程每次启动的开始入口
    /// <remarks>
    /// 这个类主要是用来帮助开发的过程中，避免去重复进行低效的排错。<br/>比如项目的中间部分有一个需要调试的内容
    /// 但每次调试都需要进行开始启动项目->运行前面的依赖部分->最后才进行到调试的部分。<br/>
    /// 这里通过让场景里的某个固定<see cref="GameObject"/>，例如摄像机上的脚本继承<see cref="PortalMgr"/>作为游戏工程的门户入口。
    /// 脚本将在游戏的开始判断游戏的工程状态
    /// </remarks>
    /// </summary>
    public abstract class KooPortalManager : KooAutoSingletonMono<KooPortalManager>
    {
        public EnvironmentMode Mode; //游戏开发阶段

        private static EnvironmentMode _sharedMode;
        private static bool _isModeSet = false;
        
        /// <summary>
        /// <para>这里是游戏门户入口</para>本框架选择的是使用<see cref="Awake"/>进行开发状态的判断
        /// <remarks>
        /// <para>
        /// 这里详细记录一下Unity中Awake函数<br/>Awake在脚本被实例化的时候就会被调用(不管脚本是否enable)，注意脚本的enable与否会影响Start函数，而且在脚本的生命周期中只会被调用一次。
        /// <br/>在所有对象的Start前调用，在Untiy的设置中找到"Scripts Execution Order"中可以设置脚本的启动顺序，官方的建议是：尽量在Awake中进行初始化操作。
        /// </para>
        /// </remarks>
        /// </summary>
        protected void Awake()
        {
            if (!_isModeSet)
            {
                _sharedMode = Mode;
                _isModeSet = true;
            }
            Mode = _sharedMode;
            switch (_sharedMode)
            {
                case EnvironmentMode.Developing:
                    //开发阶段
                    LaunchInDevelopingMode();
                    break;
                case EnvironmentMode.Testing:
                    //测试阶段
                    LaunchInTestingMode();
                    break;
                case EnvironmentMode.Production:
                    //产品阶段
                    LaunchInProductionMode();
                    break;
            }
        }

        protected abstract void LaunchInDevelopingMode();
        protected abstract void LaunchInTestingMode();
        protected abstract void LaunchInProductionMode();
    }
}