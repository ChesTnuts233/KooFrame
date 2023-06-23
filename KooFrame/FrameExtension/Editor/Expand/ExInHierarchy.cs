using UnityEngine;
using UnityEditor;
using System.Linq;

public class ExInHierarchy
{
    //Hierarchy面板下
    [MenuItem("GameObject/KooFrame/Primitive/Cube(0,1,0)", false, 0)]
    static void HierarchyCreateCube()
    {
        GameObject myCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        myCube.transform.SetPositionAndRotation(Vector3.up, Quaternion.identity);
    }

    [MenuItem("GameObject/KooFrame/Primitive/Plane50x1x50", false, 1)]
    static void HierarchyCreatePlane()
    {
        GameObject myPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        myPlane.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        myPlane.transform.localScale = new Vector3(50, 1, 50);
    }

    [MenuItem("GameObject/KooFrame/UI/KooCanvas", false, 1)]
    static void ProjectCreateKooCanvas()
    {
        GameObject myCanvas = Object.Instantiate(Resources.Load<GameObject>("KooCanvas")) ;
        myCanvas.transform.name = "KooCanvas";
    }

    [MenuItem("GameObject/KooFrame/Tools/Scene跟踪MainCamera", false, 0)]
    public static void FollowGameCamera()
    {
        var camera = Camera.allCameras.FirstOrDefault();
        SceneView.lastActiveSceneView.LookAt(camera.transform.position, camera.transform.rotation, 0.01f);
    }

    [MenuItem("GameObject/KooFrame/Tools/是否禁止在Scene中选择对象", false, 1)]
    static void CanChooseInScene()
    {
        if (ExSceneChooseObj.CanChooseInScene == false)
        {
            ExSceneChooseObj.CanChooseInScene = true;
            Debug.Log("KooHelp:" + "已经可以在场景中选择对象了哦~");
        }
        else
        {
            ExSceneChooseObj.CanChooseInScene = false;
            Debug.Log("KooHelp:" + "已经禁止在场景中选择对象了哦~");
        }
    }
}