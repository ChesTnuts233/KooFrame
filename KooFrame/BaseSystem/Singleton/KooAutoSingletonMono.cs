using UnityEngine;

namespace KooFrame.BaseSystem.Singleton
{
    /// <summary>
    /// Koo提供的自动Mono单例类
    /// 前缀Koo，这是框架提供的单例类
    /// </summary>
    /// <typeparam name="T">单例泛型</typeparam>
    public abstract class KooAutoSingletonMono<T> : MonoBehaviour where T : KooAutoSingletonMono<T>
    {
        protected static T m_Instance = null;

        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = FindObjectOfType<T>();

                    if (FindObjectsOfType<T>().Length > 1)
                    {
                        Debug.LogWarning("实例超过了一个");
                        return m_Instance;
                    }
 
                    if (m_Instance == null)
                    {
                        var instanceName = typeof(T).Name;
                        Debug.LogFormat("Instance Name: {0}", instanceName);
                        var instanceObj = GameObject.Find(instanceName);
                        if (!instanceObj) 
                        {
                            instanceObj = new GameObject(instanceName);
                        }

                        m_Instance = instanceObj.AddComponent<T>();
                        DontDestroyOnLoad(instanceObj); //保证实例不会被释放

                        Debug.LogFormat("Add New Singleton {0} in Game!", instanceName);
                    }
                    else
                    {
                        Debug.LogFormat("Already exist: {0}", m_Instance.name);
                    }
                }

                return m_Instance;
            }
        }
        
    }
}