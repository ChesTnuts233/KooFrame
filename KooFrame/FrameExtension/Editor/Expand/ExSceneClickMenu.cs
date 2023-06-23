using UnityEditor;
using UnityEngine;


public class ExSceneClickMenu
{
    public static bool OpenMyMenu = false;
    [InitializeOnLoadMethod]
    static void InitializeOnLoadMethod()
    {
        SceneView.duringSceneGui += delegate (SceneView sceneView)
        {
            if (OpenMyMenu == true)
            {
                Handles.BeginGUI();
                GUI.color = Color.red;
                GUI.Label(new Rect(0f, 0f, 350f, 15f), "你的自定义菜单开启了哦！在Scene中鼠标右键试试吧~");
                Handles.EndGUI();
            }
            Event e = Event.current;
            //鼠标右键抬起时
            if (e != null && e.button == 1 && e.type == EventType.MouseUp && OpenMyMenu == true)
            {
                Vector2 mousePosition = e.mousePosition;
                //设置菜单项
                var options = new GUIContent[]
                {
                    new GUIContent("Test1"),
                    new GUIContent("Test2"),
                    new GUIContent(""),
                    new GUIContent("Test/Test3"),
                    new GUIContent("Test/Test4"),
                };
                //设置菜单显示区域
                var selected = -1;
                var userData = Selection.activeGameObject;
                var width = 100;
                var height = 100;
                var position = new Rect(mousePosition.x, mousePosition.y - height, width, height);
                //显示菜单
                EditorUtility.DisplayCustomMenu(position, options, selected,
                    delegate (object data, string[] opt, int select)
                    {
                        Debug.Log(opt[select]);
                    }, userData);
                e.Use();
            }
        };
    }
}
