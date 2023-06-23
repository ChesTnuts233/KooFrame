using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraHelp : MonoBehaviour
{
    public static bool canHelp = false;
    public static bool canWriteRedLine = true;
    public static bool canReadPosition = true;
#if UNITY_EDITOR //这是一个宏定义标签 其中UNITY_EDITOR表示这段代码只会在Editor模式下执行 发布后被剔除
    [MenuItem("CONTEXT/CameraHelp/CameraHelp")]
    public static void OpenCameraHelp(MenuCommand command)
    {
        //CameraHelp script = (command.context as CameraHelp);
        if (canHelp == false)
        {
            canHelp = true;
            Debug.Log("CameraHelpOpen");
        }
        else
        {
            canHelp = false;
            Debug.Log("CameraHelpClose");
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (canWriteRedLine)
        {
            Gizmos.color = Color.red;
            //画线
            //Gizmos.DrawLine(transform.position, Vector3.zero);
            //Gizmos.DrawCube(Vector3.zero, new Vector3(0.1f, 0.1f, 0.1f));
            //向正前方绘制红线
            Gizmos.DrawRay(transform.position, transform.forward * 3);
        }
    }
    /// <summary>
    /// 始终出现在Scene视图中
    /// </summary>
    //private void OnDrawGizmos() { }

#endif
}
