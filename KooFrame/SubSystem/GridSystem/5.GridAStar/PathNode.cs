using GridSystem;

namespace KooFrame.SubSystem.GridSystem
{
    public class PathNode
    {
        private GridMapXY<PathNode> _grid;

        public int x;
        public int y;

        /// <summary>
        /// 从开始节点的行动代价
        /// Walking Cost from the Start Node
        /// </summary>
        public int gCost;

        /// <summary>
        /// 到达目标节点的探索代价
        /// Heuristic Cost to reach End Node
        /// </summary>
        public int hCost;

        public int fCost;

        public bool isWalkable;

        /// <summary>
        /// 来自的节点
        /// 从这个节点到当前的节点
        /// </summary>
        public PathNode cameFromNode;

        public PathNode(GridMapXY<PathNode> grid, int x, int y)
        {
            this._grid = grid;
            this.x = x;
            this.y = y;
            isWalkable = true;
        }
        
        

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }
        
        // public void SetIsWalkable(bool isWalkable) {
        //     this.isWalkable = isWalkable;
        //     _grid.TriggerGridObjectChanged(x, y);
        // }

        public override string ToString()
        {
            return x + "," + y;
        }
    }
}