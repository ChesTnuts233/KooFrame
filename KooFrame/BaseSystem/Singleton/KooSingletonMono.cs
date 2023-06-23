using System;
using UnityEngine;

namespace KooFrame.BaseSystem.Singleton
{
    public class KooSingletonMono<T> : MonoBehaviour where T : KooSingletonMono<T>
    {
        private static T m_Instance;

        public static T Instance
        {
            get
            {
                return m_Instance;
            }
        }

        protected virtual void Awake()
        {
            m_Instance = this as T;
        }
    }
}