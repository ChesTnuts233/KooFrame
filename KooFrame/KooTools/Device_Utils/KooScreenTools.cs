using KooFrame.KooManagers.UIManager;
using UnityEngine.UI;
using UnityEngine;

namespace KooFrame.KooTools.Device_Utils
{
    public class KooScreenTools
    {
        public void SetResolution(float width, float height, float matchWidthOrHeight)
        {
            if (KooUIManager.KooCanvasaGameObject == null)
            {
                Debug.LogWarning("没有找到KooCanvas");
                return;
            }

            var canvasScaler = KooUIManager.KooCanvasaGameObject.GetComponent<CanvasScaler>();
            canvasScaler.referenceResolution = new Vector2(width, height);
            canvasScaler.matchWidthOrHeight = matchWidthOrHeight;
        }
    }
}