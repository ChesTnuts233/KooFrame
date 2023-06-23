using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyUtils
{
	public static class Utils
	{
		private static readonly Vector3 Vector3zero = Vector3.zero;
		private static readonly Vector3 Vector3one = Vector3.one;
		private static readonly Vector3 Vector3yDown = new Vector3(0, -1);

		public const int sortingOrderDefault = 5000;


		// Create Text in the World
		public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault)
		{
			if (color == null) color = Color.white;
			return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
		}

		// Create Text in the World
		public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
		{
			GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
			Transform transform = gameObject.transform;
			transform.SetParent(parent, false);
			transform.localPosition = localPosition;
			TextMesh textMesh = gameObject.GetComponent<TextMesh>();
			textMesh.anchor = textAnchor;
			textMesh.alignment = textAlignment;
			textMesh.text = text;
			textMesh.fontSize = fontSize;
			textMesh.color = color;
			textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
			return textMesh;
		}

		// Get UI Position from World Position
		public static Vector2 GetWorldUIPosition(Vector3 worldPosition, Transform parent, Camera uiCamera, Camera worldCamera)
		{
			Vector3 screenPosition = worldCamera.WorldToScreenPoint(worldPosition);
			Vector3 uiCameraWorldPosition = uiCamera.ScreenToWorldPoint(screenPosition);
			Vector3 localPos = parent.InverseTransformPoint(uiCameraWorldPosition);
			return new Vector2(localPos.x, localPos.y);
		}

		// Get World Position from UI Position
		public static Vector3 GetWorldPositionFromUI()
		{
			return GetWorldPositionFromUI(Input.mousePosition, Camera.main);
		}

		public static Vector3 GetWorldPositionFromUI(Camera worldCamera)
		{
			return GetWorldPositionFromUI(Input.mousePosition, worldCamera);
		}

		public static Vector3 GetWorldPositionFromUI(Vector3 screenPosition, Camera worldCamera)
		{
			Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
			return worldPosition;
		}

		public static Vector3 GetWorldPositionFromUI_Perspective()
		{
			return GetWorldPositionFromUI_Perspective(Input.mousePosition, Camera.main);
		}

		public static Vector3 GetWorldPositionFromUI_Perspective(Camera worldCamera)
		{
			return GetWorldPositionFromUI_Perspective(Input.mousePosition, worldCamera);
		}

		public static Vector3 GetWorldPositionFromUI_Perspective(Vector3 screenPosition, Camera worldCamera)
		{
			Ray ray = worldCamera.ScreenPointToRay(screenPosition);
			Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0f));
			float distance;
			xy.Raycast(ray, out distance);
			return ray.GetPoint(distance);
		}
	}
}
