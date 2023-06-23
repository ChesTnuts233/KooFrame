using UnityEngine;

namespace KooFrame.KooTools.Device_Utils
{
    /// <summary>
    /// 设备屏幕检测
    /// </summary>
    public static class KooScreenCheck
    {

        /// <summary>
        /// 判断设备是否竖屏 如果返回true则为横屏，false则为竖屏
        /// </summary>
        /// <returns></returns>
        public static bool IsLandScape()
        {
            return Screen.width > Screen.height;
        }
        /// <summary>
        /// 获取屏幕长宽比例
        /// </summary>
        /// <returns></returns>
        public static float GetAspectRatio()
        {
            return IsLandScape() ? (float)Screen.width / Screen.height : (float)Screen.height / Screen.width;
        }

        /// <summary>
        /// 判断是否为此比例范围内 eg: IsAspectRange(16F/9);
        /// </summary>
        /// <param name="aspectRatio">设备的屏幕比例 eg: 16F/9</param>
        /// <returns></returns>
        public static bool IsAspectRange(float aspectRatio)
        {
            var aspect = GetAspectRatio();
            return aspect > (aspectRatio - 0.05) && aspect < (aspectRatio + 0.05);
        }
    }
}
