using UnityEngine;
using UnityEditor;

[CustomPreview(typeof(GameObject))]
public class OwObjectPreview : ObjectPreview
{
	//开启预览窗格
	public override bool HasPreviewGUI()
	{
		return true;
	}
	public override void OnPreviewGUI(Rect r, GUIStyle background)
	{
		//GUI.DrawTexture(r, AssetDatabase.LoadAssetAtPath<Texture>("Assets/[6]Test/test.png"));
		//GUILayout.Label("这里是重写的预览窗口哦");
	}
}
