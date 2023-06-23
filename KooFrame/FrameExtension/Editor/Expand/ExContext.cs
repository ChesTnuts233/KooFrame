using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
//这里重写了创建UIImage的方法 默认取消了射线检测
//[CustomEditor(typeof(Transform))]
#if UNITY_EDITOR //此为宏定义标签 UNITY_EDITOR表示这段代码只在Editor模式下执行 发布后剔除
public class ExContext /*: Editor*/
{
    [MenuItem("CONTEXT/Transform/把本地旋转给归零了！")]
    public static void LocalRotateReset(MenuCommand command)
    {
        var obj = command.context.GetComponent<Transform>();
        obj.transform.localRotation = Quaternion.identity;
        Debug.Log("KooHelp:" + "旋转已经归零啦！");
    }
    [MenuItem("CONTEXT/Transform/把你的本地位置设置到(0,1,0)！")]
    public static void LocalPositon(MenuCommand command)
    {
        var obj = command.context.GetComponent<Transform>();
        obj.transform.position = new Vector3(0, 1, 0);
        Debug.Log("KooHelp:" + "位置已经设置好啦！");
    }
}

#endif
