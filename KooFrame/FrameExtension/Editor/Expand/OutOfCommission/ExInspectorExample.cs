/*
#if UNITY_EDITOR
using UnityEditor;
#endif 

using UnityEngine;

public class ExInspectorExample : MonoBehaviour
{
    public Vector3 scrollPos;
    public int myID;
    public string myName;
    public GameObject prefab;
    public MyEnum myEnum = MyEnum.One;
    public bool toogle1;
    public bool toogle2;

    public enum MyEnum
    {
        One = 1,
        Two,
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(ExInspectorExample))]
public class ExInspectorEditor : Editor
{
    private bool m_EnableToogle;
    public override void OnInspectorGUI()
    {
        ExInspectorExample exInspector = target as ExInspectorExample;  //获取脚本对象
        exInspector.scrollPos = EditorGUILayout.BeginScrollView(exInspector.scrollPos, false, true); //绘制滚动条
        exInspector.myName = EditorGUILayout.TextField("text", exInspector.myName);
        exInspector.myID = EditorGUILayout.IntField("int", exInspector.myID);
        exInspector.prefab = EditorGUILayout.ObjectField("GameObject", exInspector.prefab, typeof(GameObject), true) as GameObject;

        //绘制按钮
        EditorGUILayout.BeginHorizontal();
        GUILayout.Button("1");
        GUILayout.Button("2");
        exInspector.myEnum = (ExInspectorExample.MyEnum)EditorGUILayout.EnumPopup("MyEnum:", exInspector.myEnum);
        EditorGUILayout.EndHorizontal();
        //Toogle组件
        m_EnableToogle = EditorGUILayout.BeginToggleGroup("EnableToogle", m_EnableToogle);
        exInspector.toogle1 = EditorGUILayout.Toggle("toogle1", exInspector.toogle1);
        exInspector.toogle2 = EditorGUILayout.Toggle("toogle2", exInspector.toogle2);
        EditorGUILayout.EndToggleGroup();

        EditorGUILayout.EndScrollView();
    }
}
#endif
*/