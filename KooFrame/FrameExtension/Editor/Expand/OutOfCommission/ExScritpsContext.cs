//using UnityEngine;
//#if UNITY_EDITOR
//using UnityEditor;
//#endif                             
////注意 这将是一个范例 来自拓展Context菜单 书P43
//public class ExScritpsContext : MonoBehaviour
//{
/* 范本
    public string ccontextName;
    #if UNITY_EDITOR //此为宏定义标签 UNITY_EDITOR表示这段代码只在Editor模式下执行 发布后剔除
    [MenuItem("CONTEXT/ExScritpsContext/New Context 1")]
    public static void NewContext2(MenuCommand command)
    {
        ExScritpsContext script = (command.context as ExScritpsContext);
        script.ccontextName = "Hello world!";
    }

    [ContextMenu("Remove Component")]
    void RemoveComponent()
    {
        Debug.Log("RemoveComponent");
        //等一帧再删除自己
        UnityEditor.EditorApplication.delayCall = delegate ()
        {
            DestroyImmediate(this);
        };
    }
#endif
*/
//}


