using UnityEngine;
using UnityEngine.Tilemaps;

namespace PlatformerMVC
{
    public class MarchingSquareGenerator : MonoBehaviour
    {
        public class Node
        {
            public Vector3 _position;

            public Node(Vector3 pos)
            {
                _position = pos;
            }
        }

        public class ControlNode : Node
        {
            public bool _active;

            public ControlNode(Vector3 pos, bool active) : base(pos)
            {
                _active = active;
            }
        }

        public class Square
        {
            public ControlNode TopLeft, TopRight, BottomLeft, BottomRight;

            public Square(ControlNode topLeft, ControlNode topRight, ControlNode bottomLeft, ControlNode bottomRight)
            {
                TopLeft = topLeft;
                TopRight = topRight;
                BottomLeft = bottomLeft;
                BottomRight = bottomRight;
            }
        }

        public class SquareGrid
        {
            public Square[,] Squares;

            public SquareGrid(int[,] map, float squareSize)
            {
                int nodeCountX = map.GetLength(0);
                int nodeCountY = map.GetLength(1);

                var mapWidht = nodeCountX * squareSize;
                var mapHeight = nodeCountY * squareSize;

                ControlNode[,] controlNodes = new ControlNode[nodeCountX, nodeCountY];

                for (int x = 0; x < nodeCountX; x++)
                {
                    for (int y = 0; y < nodeCountY; y++)
                    {
                        Vector3 position = new Vector3(-mapWidht / 2 + x * squareSize + squareSize / 2,
                            -mapHeight / 2 + y * squareSize + squareSize / 2);
                        controlNodes[x, y] = new ControlNode(position, map[x, y] == 1);
                    }
                }

                Squares = new Square[nodeCountX - 1, nodeCountY - 1];

                for (int x = 0; x < nodeCountX - 1; x++)
                {
                    for (int y = 0; y < nodeCountY - 1; y++)
                    {
                        Squares[x, y] = new Square(controlNodes[x, y + 1], controlNodes[x + 1, y + 1],
                            controlNodes[x + 1, y], controlNodes[x, y]);
                    }
                }
            }
        }
    }
}