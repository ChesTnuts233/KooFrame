using System.Text;
using KooFrame.KooTools.File_Utils;
using KooFrame.KooTools.Time_Utils;
using UnityEngine;


namespace KooFrame.BaseSystem
{
    public static class KooFrameInstance
    {
        public class FrameInfo
        {
            private static string _name = "KooFrame";
            private static string _version = "v0.0.3";

            private static string _description =
                "这里是Koo工具集框架的根目录文件夹\n Koo 是 Knowledge Of Organization的缩写\n所以此框架是一个知识整理用 编程学习用的框架\n这里面包含了很多开发过程中可以用到的简单工具集\n ";

            public static string Name
            {
                get => _name;
                set => _name = value;
            }

            public static string Version
            {
                get => _version;
                private set => _version = value;
            }

            public static string Description
            {
                get => _description;
                set => _description = value;
            }
        }


        #region 框架描述

        public static string GetFrameDescription()
        {
            StringBuilder frameDescription = null;
            frameDescription.AppendLine("这里是Koo工具集框架的根目录文件夹");
            frameDescription.AppendLine("Koo 是 Knowledge Of Organization的缩写\n所以此框架是一个知识整理用 编程学习用的框架");
            frameDescription.AppendLine("\n这里面包含了很多开发过程中可以用到的简单工具集\n ");

            return frameDescription.ToString();
        }

        #endregion

        #region 框架名称操作

        /// <summary>
        /// 返回框架工程文件名(有5个默认参数,默认都为false)
        /// </summary>
        /// <param name="isAddDataPath">是否在名称前加到工程文件的绝对路径</param>
        /// <param name="isAddAssetsBefore">是否在本名前加Assets/路径</param>
        /// <param name="isAddVersion">是否添加版本号</param>
        /// <param name="isAddTime">是否加时间</param>
        /// <param name="isAddFileType">是否加类型后缀</param>
        /// <returns>返回被修饰过的框架包名称</returns>
        public static string GetFrameworkFileName
        (
            bool isAddDataPath = false, //是否在名称前加到工程文件的绝对路径
            bool isAddAssetsBefore = false, //是否在本名前加Assets/路径
            bool isAddVersion = false, //是否添加版本号
            bool isAddTime = false, //是否加时间
            bool isAddFileType = false //是否加类型后缀
        )
        {
            StringBuilder sb = new StringBuilder();
            if (isAddDataPath)
            {
                sb.Append(KooFileOperators.GetLocalPathTo());
            }

            if (isAddAssetsBefore)
            {
                sb.Append("Assets/");
            }

            sb.Append(FrameInfo.Name);
            //frameworkName = "KooFramework";
            if (isAddVersion)
            {
                sb.Append("_" + FrameInfo.Version);
            }

            if (isAddTime)
            {
                sb.Append("_" + KooDataTime.GetFormatNowTime(is12Time: false));
            }

            if (isAddFileType)
            {
                sb.Append(".unitypackage");
            }

            string name = sb.ToString();
            return name;
        }

        /// <summary>
        /// 复制框架名称到剪贴板(有5个默认参数，默认都为false)
        /// </summary>
        public static void GetCopyFrameworkFileName
        (
            bool isAddDataPath = false, //是否在名称前加到工程文件的绝对路径
            bool isAddAssetsBefore = false, //是否在本名前加Assets/路径
            bool isAddVersion = false, //是否添加版本号
            bool isAddTime = false, //是否加时间
            bool isAddFileType = false //是否加类型后缀
        )
        {
            GUIUtility.systemCopyBuffer = GetFrameworkFileName(isAddDataPath, isAddAssetsBefore,
                isAddVersion, isAddTime, isAddFileType); //复制字符串到剪贴板
        }

        #endregion

#if UNITY_EDITOR
        #region 框架打包

        /// <summary>
        /// 生成KooFramework框架的UnityPackage包在项目目录下(同时会把框架名称复制到剪贴板上)
        /// </summary>
        //[MenuItem("Assets/My Tools/生成框架UnityPackage", false, 1)]
        public static void GeneratorUnityPackage()
        {
            UnityEditor.AssetDatabase.ExportPackage(GetFrameworkFileName(isAddAssetsBefore: true),
                GetFrameworkFileName(isAddDataPath: true, isAddAssetsBefore: false, isAddVersion: true, isAddTime: true,
                    isAddFileType: true),
                UnityEditor.ExportPackageOptions.Recurse);
            KooFileOperators.OpenLocalPath();
            GetCopyFrameworkFileName(isAddTime: true, isAddVersion: true); //复制文件名到剪贴板
        }

        #endregion

#endif
    }
}