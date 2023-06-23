//using UnityEditor;
//using UnityEngine;
//// 3-17 常驻辅助UI
//public class OwSceneGUI
//{
//    [InitializeOnLoadMethod]
//    static void InitializeOnLoadMethod()
//    {
//        SceneView.duringSceneGui += delegate (SceneView sceneView)
//        {
//            Handles.BeginGUI();
//            GUI.Label(new Rect(0f, 0f, 50f, 15f), "标题");
//            if (GUI.Button(new Rect(0f, 20f, 50f, 50f), "Test"))
//            {
//                Debug.Log("这是一个按钮哟！");
//            }
//            Handles.EndGUI();
//        };
//    }
//}