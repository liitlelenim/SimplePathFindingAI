using System.Numerics;
namespace PathFindingAI
{
    internal class PathFinder
    {
        private readonly Board _board;
        private readonly Visualizer _visualizer;
        public PathFinder(Board board, Visualizer visualizer)
        {
            _board = board;
            _visualizer = visualizer;
        }
        public List<Vector2>? AStarSearch()
        {
            _visualizer.Clear();
            BoardNode? startPoint = null, endPoint = null;
            BoardNode[] nodes =
                new BoardNode[(_board.Cells.GetLength(0)) * _board.Cells.GetLength(1)];
            for (int x = 0; x < _board.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < _board.Cells.GetLength(1); y++)
                {
                    nodes[(x * _board.Cells.GetLength(1)) + y] = new BoardNode(new Vector2(x, y), _board.Cells[x, y] != CellType.Wall);
                    if (_board.Cells[x, y] == CellType.StartPoint)
                    {
                        startPoint = nodes[(x * _board.Cells.GetLength(1)) + y];
                    }
                    else if (_board.Cells[x, y] == CellType.Target)
                    {
                        endPoint = nodes[(x * _board.Cells.GetLength(1)) + y];
                    }
                }
            }
            if (startPoint == null || endPoint == null)
            {
                throw new ArgumentNullException("No start point or endpoint specified");
            }

            List<BoardNode> openNodes = new List<BoardNode>() { startPoint };
            List<BoardNode> closedNodes = new List<BoardNode>();
            while (openNodes.Count > 0)
            {

                _visualizer.OpenNodesPositons = openNodes.Select(node => node.Position).ToList();
                _visualizer.ClosedNodesPositons = closedNodes.Select(node => node.Position).ToList();
                _visualizer.OpenNodesPositons.Remove(startPoint.Position);
                _visualizer.ClosedNodesPositons.Remove(startPoint.Position);

                if (_visualizer.Enabled)
                {
                    Drawer.DrawCall();
                }
                BoardNode leastFNode = openNodes[0];
                foreach (var node in openNodes)
                {
                    if (node.GetFValue(startPoint, endPoint.Position) < leastFNode.GetFValue(startPoint, endPoint.Position))
                    {
                        leastFNode = node;
                    }
                }
                openNodes.Remove(leastFNode);
                List<BoardNode> successors = new List<BoardNode>();
                for (int i = 0; i < nodes.Length; i++)
                {
                    if (
                        nodes[i].Position == leastFNode.Position + new Vector2(1, 0) ||
                        nodes[i].Position == leastFNode.Position + new Vector2(-1, 0) ||
                        nodes[i].Position == leastFNode.Position + new Vector2(0, 1) ||
                        nodes[i].Position == leastFNode.Position + new Vector2(0, -1))
                    {
                        if (!closedNodes.Contains(nodes[i]))
                        {
                            nodes[i].Parent = leastFNode;
                            successors.Add(nodes[i]);
                            if (successors.Count > 3) { break; }
                        }
                    }
                }
                foreach (BoardNode node in successors)
                {
                    if (!node.Traversable) { continue; }
                    if (node.Position == endPoint.Position)
                    {
                        return TraverseNodesPath(node, startPoint);
                    }
                    bool hasAlreadyBetterOption = false;
                    int nodeFValue = node.GetFValue(startPoint, endPoint.Position);
                    for (int i = 0; i < closedNodes.Count; i++)
                    {
                        if (closedNodes[i].Position == node.Position)
                        {
                            if (closedNodes[i].GetFValue(startPoint, endPoint.Position) < nodeFValue)
                            {
                                hasAlreadyBetterOption = true;
                                break;
                            }
                        }
                    }
                    if (hasAlreadyBetterOption) { continue; }
                    for (int i = 0; i < openNodes.Count; i++)
                    {
                        if (openNodes[i].Position == node.Position)
                        {
                            if (openNodes[i].GetFValue(startPoint, endPoint.Position) < nodeFValue)
                            {
                                hasAlreadyBetterOption = true;
                                break;
                            }
                        }
                    }

                    if (!hasAlreadyBetterOption)
                    {
                        if (!openNodes.Contains(node))
                        {
                            openNodes.Add(node);
                        }
                    }
                }
                if (!closedNodes.Contains(leastFNode))
                {
                    closedNodes.Add(leastFNode);
                }
            }
            return null;
        }
        private List<Vector2> TraverseNodesPath(BoardNode node, BoardNode startPoint)
        {
            List<Vector2> foundPath = new List<Vector2>();
            node = node.Parent;
            while (node != startPoint)
            {
                foundPath.Add(node.Position);
                node = node.Parent;
            }
            return foundPath;
        }
    }
    class BoardNode
    {
        public Vector2 Position { get; init; }
        public bool Traversable { get; init; }
        public BoardNode? Parent { get; set; }

        private int _gValueMemo = -1;
        public int DistanceTo(Vector2 position)
        {
            return (int)(Math.Abs(Position.X - position.X) + Math.Abs(Position.Y - position.Y));
        }
        public int GetFValue(BoardNode startingNode, Vector2 targetPosition)
        {
            return GetGValue(startingNode) + DistanceTo(targetPosition);
        }
        public int GetGValue(BoardNode startingNode)
        {
            if (this == startingNode) { return 0; }
            if (_gValueMemo != -1) { return _gValueMemo; }
            BoardNode currentNode = this;
            int length = 1;
            while (currentNode.Position != startingNode.Position)
            {
                currentNode = currentNode.Parent;
                length++;
            }
            _gValueMemo = length;
            return length;
        }

        public BoardNode(Vector2 position, bool traversable)
        {
            Position = position;
            Traversable = traversable;
        }
    }
}

