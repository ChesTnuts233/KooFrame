using UnityEngine;

namespace KooFrame
{
    public abstract class PoolObject : MonoBehaviour,IDelayPush
    {
        [Header("延迟回收时间")]
        [SerializeField] private float RecycleTime;
        public virtual void OnEnable()
        {
            Recycle(RecycleTime);
        }

        public virtual void Recycle(float delayTime)
        {
            Invoke("Push",delayTime);
        }
        
        public void Push()
        {
            PoolMgr.Instance.PushObj(this.gameObject.name, this.gameObject);
        }
    }
}