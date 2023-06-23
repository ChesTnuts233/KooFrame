#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
//此为代码范例  关键在于等一帧实现功能 避免报错 具体使用需要挂载在物体上 并改类名 P44 
#if UNITY_EDITOR
public class OwRemoveComponent : MonoBehaviour
{
    [ContextMenu("My Remove Component")]
    void RemoveComponent()
    {
        Debug.Log("MyRemoveComponent");
        //等一帧再删除自己
        UnityEditor.EditorApplication.delayCall = delegate ()
        {
            DestroyImmediate(this);
        };
    }
}
#endif