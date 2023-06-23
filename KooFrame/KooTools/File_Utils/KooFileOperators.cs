using System.IO;
using UnityEngine;

namespace KooFrame.KooTools.File_Utils
{
    /// <summary>
    /// <para>文件工具类</para>
    /// <para>1.包括了对编辑器中的文件进行操作</para>
    /// <para>2.包括了对UnityPackage的简化操作</para>
    /// </summary>
    public static class KooFileOperators
    {
        public static void OpenLocalPath()
        {
            Application.OpenURL("file:////" + GetLocalPathTo());
        }

        /// <summary>
        /// 获取本地路径到(xxx/xxx/)包括"/"
        /// </summary>
        /// <returns></returns>
        public static string GetLocalPathTo()
        {
            return Directory.GetParent(Application.dataPath)?.FullName + "/";
        }

        /// <summary>
        /// 获取Assets路径到(xxx/xxx/)包括"/"
        /// </summary>
        /// <returns></returns>
        public static string GetAssetsPathTo()
        {
            return Application.dataPath + "/";
        }

#if UNITY_EDITOR
        /// <summary>
        /// 对UnityPackage导出的简化后方法
        /// </summary>
        /// <param name="assetPathName">资源路径</param>
        /// <param name="fileName">目标文件名</param>
        public static void ExportPackage(string assetPathName, string fileName)
        {
            UnityEditor.AssetDatabase.ExportPackage(assetPathName, fileName, UnityEditor.ExportPackageOptions.Recurse);
        }
#endif
    }
}