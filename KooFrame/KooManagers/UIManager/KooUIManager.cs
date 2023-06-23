using KooFrame.BaseSystem.Singleton;
using KooFrame.KooTools.Object_Utils;
using UnityEngine;
using UnityEngine.UI;

namespace KooFrame.KooManagers.UIManager
{
    public class KooUIManager : KooSingleton<KooUIManager>
    {
        private static GameObject _kooCanvasaGameObject;

        public static GameObject KooCanvasaGameObject
        {
            get { return _kooCanvasaGameObject; }
            private set { _kooCanvasaGameObject = value; }
        }
        
        // public KooUIManager()
        // {
        //     _kooCanvasaGameObject = GetKooCanvas();
        //     
        // }

        public void LoadPanel(string panelName)
        {
            //从Resources文件夹中获取Panel预制体，并且把Panel的父对象设置到KooCanvas上
            var panelPrefab = Object.Instantiate(Resources.Load<GameObject>(panelName), _kooCanvasaGameObject.transform, true);
            //panelPrefab.transform.name = KooGameObjectTools.GetSimplyPrefabName(panelPrefab); //使用Koo提供的简化名称方法（没必要）只是告知有这个方法
            panelPrefab.transform.name = panelName; //重置panel名字 去除"(Clone)"
            var panelRectTrans = panelPrefab.transform as RectTransform;


            FormalPanel(panelPrefab); //规范化Panel的RectTrans
        }


        public void FormalPanel(GameObject panel)
        {
            var panelRectTrans = panel.transform.transform as RectTransform;
            if (panelRectTrans == null) return;
            panelRectTrans.offsetMin = Vector2.zero;
            panelRectTrans.offsetMax = Vector2.zero;
            panelRectTrans.anchoredPosition3D = Vector3.zero;
            panelRectTrans.anchorMin = Vector2.zero;
            panelRectTrans.anchorMax = Vector2.one;
        }


        public GameObject GetKooCanvas()
        {
            if (!GameObject.FindWithTag("KooCanvas"))
            {
                Debug.LogWarning("没有找到KooCanvas，请在Hierarchy的右键菜单中找到UI/KooCanvas");
                return null;
            }

            var myCanvas = GameObject.FindWithTag("KooCanvas");
            return myCanvas;
        }
    }
}