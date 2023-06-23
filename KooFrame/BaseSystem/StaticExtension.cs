using UnityEngine;

namespace KooFrame.BaseSystem
{
    public static class StaticExtension
    {
        #region 静态this拓展

        public static void SaySelf(this object selfObj)
        {
            Debug.Log(selfObj);
        }


        #endregion
    }
}