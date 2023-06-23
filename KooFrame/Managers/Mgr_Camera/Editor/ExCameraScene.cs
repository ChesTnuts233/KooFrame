using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Camera))]
public class ExCameraScene : Editor
{
    void OnSceneGUI()
    {
        Camera camera = target as Camera;
        if (camera != null)
        {
            if (CameraHelp.canReadPosition == true)
            {
                Handles.color = Color.yellow;  //这段不知为何 设置不了颜色
                Handles.Label(camera.transform.position, camera.transform.position.ToString());
            }
            Handles.BeginGUI();
            GUILayout.BeginHorizontal();
            GUI.backgroundColor = Color.white;
            if (GUILayout.Button("Scene跟随摄像机", GUILayout.Width(120f)))
            {
                ExInHierarchy.FollowGameCamera();
            }
            if (GUILayout.Button("开关摄像机辅助", GUILayout.Width(120f)))
            {
                if (CameraHelp.canHelp == false)
                {
                    CameraHelp.canHelp = true;
                    CameraHelp.canReadPosition = true;
                    CameraHelp.canWriteRedLine = true;
                }
                else
                {
                    CameraHelp.canHelp = false;
                    CameraHelp.canReadPosition = false;
                    CameraHelp.canWriteRedLine = false;
                }
            }
            if (CameraHelp.canHelp == true)
            {
                if (GUILayout.Button("开关红线", GUILayout.Width(80f)))
                {
                    if (CameraHelp.canWriteRedLine == false)
                    {
                        CameraHelp.canWriteRedLine = true;
                    }
                    else
                    {
                        CameraHelp.canWriteRedLine = false;
                    }
                }
                if (GUILayout.Button("开关坐标", GUILayout.Width(80f)))
                {
                    if (CameraHelp.canReadPosition == false)
                    {
                        CameraHelp.canReadPosition = true;
                    }
                    else
                    {
                        CameraHelp.canReadPosition = false;
                    }
                }
            }
            //GUILayout.Label("Label");
            GUILayout.EndHorizontal();
            Handles.EndGUI();
        }
    }
}
