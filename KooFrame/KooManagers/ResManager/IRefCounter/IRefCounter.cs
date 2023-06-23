using UnityEngine;

namespace KooFrame
{
    public interface IRefCounter
    {
        int RefCount { get; }
        void Retain(object refOwner = null);
        void Release(object refOwner = null);
    }
    
}