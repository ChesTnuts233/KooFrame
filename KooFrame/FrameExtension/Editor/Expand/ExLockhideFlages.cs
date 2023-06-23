using UnityEditor;
using UnityEngine;

//3.3.3 P40 3-12
public class ExLockhideFlages
{
    [MenuItem("GameObject/3D Object/Lock/Lock", false, 0)]
    static void Lock()
    {
        if (Selection.gameObjects != null)
        {
            foreach (var gameObject in Selection.gameObjects)
            {
                gameObject.hideFlags = HideFlags.NotEditable;
            }
        }
    }
    [MenuItem("GameObject/3D Object/Lock/UnLock", false, 1)]
    static void UnLock()
    {
        if (Selection.gameObjects != null)
        {
            foreach (var gameObject in Selection.gameObjects)
            {
                gameObject.hideFlags = HideFlags.None;
            }
        }
    }
}

////HideFlags 可以使用按位或（|）保持多个属性
//HideFlags.None;                 //清除状态
//HideFlags.DontSave;             //设置对象不会被保存（仅编辑模式下使用，运行时被剔除）
//HideFlags.DontSaveInBuild;      //设置对象构建后不会被保存
//HideFlags.DontSaveInEditor;     //设置对象编辑模式下不会被保存
//HideFlags.DontUnloadUnusedAsset;//设置对象不会被Resource.UnloadUnusedAssets()卸载无用资源时被卸掉
//HideFlags.HideAndDontSave;      //设置对象隐藏，并且不会被保存
//HideFlags.HideInHierarchy;      //设置对象在层次视图中隐藏
//HideFlags.HideInInspector;      //设置对象在控制面板视图中隐藏
//HideFlags.NotEditable;          //设置对象不可被编辑