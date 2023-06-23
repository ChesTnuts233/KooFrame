//using UnityEditor;
//using UnityEngine;
////P50 3-20
//public class ExRootMenu
//{
//	[MenuItem("MyMenu/Test/Test1", false, 1)]
//	static void Test1()
//	{

//	}
//	//菜单排序
//	[MenuItem("MyMenu/Test/Test0", false, 0)]
//	static void Test0()
//	{

//	}
//	[MenuItem("MyMenu/Test/Test/2", false, 12)]
//	static void Test2()
//	{

//	}
//	[MenuItem("MyMenu/Test/Test/2", true, 20)]
//	static bool Test2Validation()
//	{
//		//false 表示 Root/Test/2 菜单将置灰，既不可点击
//		return false;
//	}
//	[MenuItem("MyMenu/Test/Test3", false, 3)]
//	static void Test3()
//	{
//		//勾选框中的菜单
//		var menuPath = "MyMenu/Test/Test3";
//		bool mchecked = Menu.GetChecked(menuPath);
//		Menu.SetChecked(menuPath, !mchecked);
//	}

//}
