using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class GetBuildInIcon : EditorWindow
{
	//[MenuItem("MyMenu/OpenBuild-InIcon", false, 2)]
	public static void OpenBuildInIcon()
	{
		EditorWindow.GetWindow<GetBuildInIcon>("icons");
	}
	private Vector2 m_Scroll;
	private List<string> m_Icons = null;
	private void Awake()
	{
		m_Icons = new List<string>();
		Texture2D[] t = Resources.FindObjectsOfTypeAll<Texture2D>();
		foreach (Texture2D item in t)
		{
			Debug.unityLogger.logEnabled = false;
			GUIContent gc = EditorGUIUtility.IconContent(item.name);
			Debug.unityLogger.logEnabled = true;
			if (gc != null && gc.image != null)
			{
				m_Icons.Add(item.name);
			}
		}
		Debug.Log(m_Icons.Count);
	}
	private void OnGUI()
	{
		m_Scroll = GUILayout.BeginScrollView(m_Scroll);
		float width = 50f;
		int count = (int)(position.width / width);
		for (int i = 0; i < m_Icons.Count; i += count)
		{
			GUILayout.BeginHorizontal();
			for (int j = 0; j < count; j++)
			{
				int index = i + j;
				if (index < m_Icons.Count)
				{
					GUILayout.Button(EditorGUIUtility.IconContent(m_Icons[index]),
					GUILayout.Width(width), GUILayout.Height(30));
				}
			}
			GUILayout.EndHorizontal();
		}
		EditorGUILayout.EndScrollView();
	}

}
