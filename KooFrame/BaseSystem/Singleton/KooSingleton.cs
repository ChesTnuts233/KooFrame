namespace KooFrame.BaseSystem.Singleton
{
    /// <summary>
    /// Koo框架提供的单例类
    /// 前缀Koo，这是框架提供的单例类
    /// </summary>
    /// <typeparam name="T">单例的类型</typeparam>
    public abstract class KooSingleton<T> where T : KooSingleton<T>, new()
    {
        private static T _instance; //单例实例

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }

        // private KooSingleton()
        // {
        //     
        // }
    }
}