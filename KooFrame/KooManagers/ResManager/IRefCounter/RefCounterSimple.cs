using UnityEngine;

namespace KooFrame
{
    public class RefCounterSimple : IRefCounter
    {
        public RefCounterSimple()
        {
            RefCount = 0;
        }

        public int RefCount { get; private set; }

        public void Retain(object refOwner = null)
        {
            ++RefCount;
        }

        public void Release(object refOwner = null)
        {
            --RefCount;
            if (RefCount==0)
            {
                OnZeroRef();
            }

            if (RefCount<0)
            {
                RefCount = 0;
                Debug.Log("计数器数值小于零，自动归零");
            }
        }

        protected virtual void OnZeroRef()
        {
            
        }
    }
}