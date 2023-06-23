using System.Text;
using UnityEngine;

namespace KooFrame.KooTools.Object_Utils
{
    public static class KooGameObjectTools
    {
        public static string GetSimplyPrefabName(GameObject prefab)
        {
            string name = prefab.transform.name;
            if (!name.Contains("(Clone)")) return name;
            StringBuilder nameBuilder = new StringBuilder();
            nameBuilder.Append(name);
            if (nameBuilder.Length >= 7)
            {
                nameBuilder.Remove(nameBuilder.Length - 7, 7);
            }

            return nameBuilder.ToString();
        }

        public static GameObject GetSimplyNamePrefab(GameObject prefab)
        {
            string name = prefab.transform.name;
            if (!name.Contains("(Clone)")) return prefab;
            StringBuilder nameBuilder = new StringBuilder();
            nameBuilder.Append(name);
            if (nameBuilder.Length >= 7)
            {
                nameBuilder.Remove(nameBuilder.Length - 7, 7);
            }
            prefab.transform.name = nameBuilder.ToString();
            return prefab;
        }
        
    }
}