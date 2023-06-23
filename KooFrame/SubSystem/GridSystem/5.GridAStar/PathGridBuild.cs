using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

namespace KooFrame.SubSystem.GridSystem
{
    public class PathGridBuild : MonoBehaviour
    {
        private PathFinding pathFinding;

        private void Start()
        {
            pathFinding = new PathFinding(10, 10);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
                pathFinding.Grid.GetXY(mouseWorldPosition, out int x, out int y);
                List<PathNode> path = pathFinding.AStarFindPath(0, 0, x, y);
                if (path != null) {
                    for (int i=0; i<path.Count - 1; i++) {
                        Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                    }
                }
            }
        }
    }
}