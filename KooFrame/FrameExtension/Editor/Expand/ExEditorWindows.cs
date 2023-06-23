using UnityEditor;
using UnityEngine;

public class ExEditorWindows : EditorWindow, IHasCustomMenu
{
	private Texture m_MyTexture = null;
	private float m_MyFloat = 0.5f;
	private GameObject m_MyGo;
	private Editor m_MyEditor;


	//[MenuItem("MyMenu/Open My Window", false, 1)]
	static void Init()
	{
		ExEditorWindows window = (ExEditorWindows)EditorWindow.GetWindow(typeof(ExEditorWindows));
		window.Show();
	}


	private void Awake()
	{
		//窗口初始化时调用
		m_MyTexture = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Icon/test.png");
	}
	private void OnGUI()
	{
		GUILayout.Label("Hello World!!", EditorStyles.boldLabel);
		m_MyFloat = EditorGUILayout.Slider("Slider", m_MyFloat, -5, 5);
		GUI.DrawTexture(new Rect(0, 30, 100, 100), m_MyTexture);

		//设置一个游戏对象
		m_MyGo = (GameObject)EditorGUILayout.ObjectField(m_MyGo, typeof(GameObject), true);

		if (m_MyGo != null)
		{
			if (m_MyEditor == null)
			{
				m_MyEditor = Editor.CreateEditor(m_MyGo); //创建Editor实例
			}
			//预览
			m_MyEditor.OnPreviewGUI(GUILayoutUtility.GetRect(500, 500), EditorStyles.whiteLabel);
		}
	}
	//窗口销毁时调用
	private void OnDestroy()
	{

	}
	//窗口拥有焦点时调用
	private void OnFocus()
	{

	}
	//Hierarchy视图发生改变的时候调用
	private void OnHierarchyChange()
	{

	}
	//Inspector 每帧更新
	private void OnInspectorUpdate()
	{

	}
	//失去焦点
	private void OnLostFocus()
	{

	}
	//Project视图发生改变的时候调用
	private void OnProjectChange()
	{

	}
	//在Hierarchy或者Project视图中选择一个对象的时候调用
	private void OnSelectionChange()
	{

	}
	//每帧更新
	private void Update()
	{

	}
	//EditorWindows下拉菜单 P56 3-25
	void IHasCustomMenu.AddItemsToMenu(UnityEditor.GenericMenu menu)
	{
		menu.AddDisabledItem(new GUIContent("Disable"));
		menu.AddItem(new GUIContent("Test1"), true, () =>
		{
			Debug.Log("Test1");
		});
		menu.AddItem(new GUIContent("Test2"), true, () =>
		{
			Debug.Log("Test2");
		});
		menu.AddSeparator("Test/");
		menu.AddItem(new GUIContent("Test/Test3"), true, () =>
		{
			Debug.Log("Test3");
		});
	}



}
