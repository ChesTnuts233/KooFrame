using UnityEngine;

namespace KooFrame.KooTools.Device_Utils
{
    public static class KooSystemTool
    {
        /// <summary>
        /// 复制字符串到系统剪贴板上
        /// </summary>
        /// <param name="copyText"></param>
        public static void CopyText(string copyText)
        {
            GUIUtility.systemCopyBuffer = copyText;
        }
    
    }
}

