using UnityEditor;
using UnityEngine;
//拓展全局自定义快捷键 P52 3-22
public class ExHotKey
{
	//[MenuItem("GameObject/HotKey &#d", false, -1)]  Ctrl+Shift+D 全局快捷键
	//[MenuItem("GameObject/HotKey ", false, -1)]
	private static void HotKey()
	{
		if (ExSceneClickMenu.OpenMyMenu == false)
		{
			ExSceneClickMenu.OpenMyMenu = true;
			Debug.Log("KooHelp:" + "你的自定义菜单开启了哦！在Scene中鼠标右键试试吧~");
		}
		else
		{
			ExSceneClickMenu.OpenMyMenu = false;
			Debug.Log("KooHelp:" + "你的自定义菜单关闭惹！右键Scene场景也没用啦！");
		}
	}
}
