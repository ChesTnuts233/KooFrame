using System.Collections.Generic;
using GridSystem;
using UnityEngine;

namespace KooFrame.SubSystem.GridSystem
{
    public class PathFinding
    {
        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        public static PathFinding Instance { get; private set; }

        private GridMapXY<PathNode> grid;
        private List<PathNode> openList;
        private List<PathNode> closeList;

        public GridMapXY<PathNode> Grid
        {
            get => grid;
            set => grid = value;
        }

        public PathFinding(int width, int height)
        {
            Instance = this;
            grid = new GridMapXY<PathNode>(width, height, 10f, Vector3.zero,
                (GridMapXY<PathNode> grid, int x, int y) => new PathNode(grid, x, y));
        }

        public List<Vector3> AStarFindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
        {
            grid.GetXY(startWorldPosition, out int startX, out int startY);
            grid.GetXY(endWorldPosition, out int endX, out int endY);
            List<PathNode> path = AStarFindPath(startX, startY, endX, endY);
            if (path == null)
            {
                return null;
            }

            List<Vector3> vectorPath = new List<Vector3>();
            foreach (PathNode pathNode in path)
            {
                vectorPath.Add(new Vector3(pathNode.x, pathNode.y) * grid.CellSize +
                               Vector3.one * grid.CellSize * .5f);
            }

            return vectorPath;
        }

        // public List<PathNode> BreadFirst(Vector2Int start, Vector2Int end)
        // {
        //     
        // }

        /// <summary>
        /// A*算法寻找路径
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <returns></returns>
        public List<PathNode> AStarFindPath(int startX, int startY, int endX, int endY)
        {
            PathNode startNode = grid.GetGridObject(startX, startY);
            PathNode endNode = grid.GetGridObject(endX, endY);

            if (startNode == null || endNode == null)
            {
                // 无效路径
                Debug.Log("无效路径");
                return null;
            }

            openList = new List<PathNode>() { startNode };
            closeList = new List<PathNode>();

            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    PathNode pathNode = grid.GetGridObject(x, y);
                    pathNode.gCost = int.MaxValue;
                    pathNode.CalculateFCost();
                    pathNode.cameFromNode = null;
                }
            }

            startNode.gCost = 0;
            startNode.hCost = CalculateDistanceCost(startNode, endNode); //计算到目标节点的探索代价
            startNode.CalculateFCost();

            while (openList.Count > 0)
            {
                PathNode currentNode = GetLowestFCostNode(openList); //从openList中找到F值最低的节点
                if (currentNode == endNode)
                {
                    //到达终点节点
                    return CalculatePath(endNode);
                }

                openList.Remove(currentNode);
                closeList.Add(currentNode);

                foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
                {
                    if (closeList.Contains(neighbourNode)) continue;

                    if (!neighbourNode.isWalkable)
                    {
                        closeList.Add(neighbourNode);
                        continue;
                    }

                    int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                    if (tentativeGCost < neighbourNode.gCost)
                    {
                        neighbourNode.cameFromNode = currentNode;
                        neighbourNode.gCost = tentativeGCost;
                        neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                        neighbourNode.CalculateFCost();

                        if (!openList.Contains(neighbourNode))
                        {
                            openList.Add(neighbourNode);
                        }
                    }
                }
            }

            //在OpenList之外的节点 (找不到节点)
            return null;
        }

        private List<PathNode> GetNeighbourList(PathNode currentNode)
        {
            List<PathNode> neighbourList = new List<PathNode>();
            if (currentNode.x - 1 >= 0)
            {
                //Left
                neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
                //left down
                if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
                //left up
                if (currentNode.y + 1 < grid.Height) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
            }

            if (currentNode.x + 1 < grid.Width)
            {
                //right
                neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
                //right down
                if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
                //right up
                if (currentNode.y + 1 < grid.Height) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
            }

            //Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
            //Up
            if (currentNode.y + 1 < grid.Height) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

            return neighbourList;
        }

        private PathNode GetNode(int x, int y)
        {
            return grid.GetGridObject(x, y);
        }

        private List<PathNode> CalculatePath(PathNode endNode)
        {
            List<PathNode> pathNodes = new List<PathNode>();
            pathNodes.Add(endNode);
            PathNode currentNode = endNode;
            while (currentNode.cameFromNode != null)
            {
                pathNodes.Add(currentNode.cameFromNode);
                currentNode = currentNode.cameFromNode;
            }

            pathNodes.Reverse();
            return pathNodes;
        }

        private int CalculateDistanceCost(PathNode a, PathNode b)
        {
            int xDistance = Mathf.Abs(a.x - b.x);
            int yDistance = Mathf.Abs(a.y - b.y);
            int remaining = Mathf.Abs(xDistance - yDistance);
            //将对角线移动成本 + 水平或垂直移动成本
            return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
        }

        private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
        {
            PathNode lowestFCostNode = pathNodeList[0];
            for (int i = 0; i < pathNodeList.Count; i++)
            {
                if (pathNodeList[i].fCost < lowestFCostNode.fCost)
                {
                    lowestFCostNode = pathNodeList[i];
                }
            }

            return lowestFCostNode;
        }
    }
}