//using UnityEditor;
//using UnityEngine;
//// 现在用不到
//public class ExMyMonitor : UnityEditor.AssetModificationProcessor
//{
//    public static bool isOpenMyMonitor = false;
//    [InitializeOnLoadMethod]
//    static void InitializeOnLoadMethod()
//    {
//        //全局监听Project视图下的资源是否发生变化
//        EditorApplication.projectChanged += delegate ()
//        {
//            Debug.Log("change");
//        };
//    }
//    //监听事件
//    public static bool SetIsOpenForEdit(string assetPath, out string message)
//    {
//        message = null;
//        Debug.LogFormat("打开资源 Path:{0}", assetPath);
//        //true 表示资源可以打开，反之不允许在Unity中打开资源
//        return true;
//    }
//    public static void OnWillCreateAsset(string path)
//    {
//        Debug.LogFormat("资源即将被创建 path:{0}", path);
//    }
//    public static string[] OnWillSaveAssets(string[] paths)
//    {
//        if (paths != null)
//        {
//            Debug.LogFormat("资源即将被保存 path:{0}", string.Join(",", paths));
//        }
//        return paths;
//    }
//    public static AssetMoveResult OnWillMoveAsset(string oldPath, string newPath)
//    {
//        Debug.LogFormat("资源被移动from: {0} to :{1}", oldPath, newPath);
//        //return AssetMoveResult.DidMove; //表示该资源不可以移动
//        return AssetMoveResult.DidNotMove;
//    }
//    public static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions option)
//    {
//        Debug.LogFormat("资源即将被删除delete:{0}", assetPath);
//        //return AssetDeleteResult.DidDelete; //表示该资源不可被删除.
//        return AssetDeleteResult.DidNotDelete;
//    }

//}
