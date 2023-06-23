using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class OwCreateImage
{
    [MenuItem("GameObject/UI/Image-RaycastTarget-Off")]
    static void CreateImage()
    {
        if (Selection.activeTransform)
        {
            if (Selection.activeTransform.GetComponentInParent<Canvas>())
            {
                Image image = new GameObject("image").AddComponent<Image>();
                image.raycastTarget = false;
                image.transform.SetParent(Selection.activeTransform, false);
                //设置选中状态
                Selection.activeTransform = image.transform;
            }
        }
    }
}
