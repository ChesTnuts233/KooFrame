using System;
using Object = UnityEngine.Object;

namespace KooFrame
{
    public abstract class Res : RefCounterSimple
    {
        public Object Asset { get; protected set; }

        public string Name { get; protected set; }
        

        /// <summary>
        /// 同步加载
        /// </summary>
        /// <returns></returns>
        public abstract bool LoadSync();

        /// <summary>
        /// 异步加载
        /// </summary>
        /// <param name="onLoaded"></param>
        public abstract void LoadAsync(Action<Res> onLoaded);

        protected abstract void OnReleaseRes();

        protected override void OnZeroRef()
        {
            OnReleaseRes();
        }
    }
}