using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Camera))]
public class ExMyCamera : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("拓展按钮"))
        {
            Debug.Log("test");
        }
        base.OnInspectorGUI();          //是否绘制父类原有元素
    }
}

