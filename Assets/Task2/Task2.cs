using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    enum JewelKind
    {
        Empty,
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Indigo,
        Violet
    }

    enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    struct Move
    {
        public int x;
        public int y;
        public MoveDirection direction;
    }

    int GetWidth()
    {
        return 10;
    }
    int GetHeight()
    {
        return 10;
    }
    JewelKind GetJewel(int x, int y)
    {
        return JewelKind.Empty;
    }
    void SetJewel(int x, int y, JewelKind kind)
    {

    }

    Move CalculateBestMoveForBoard()
    {
        // Note: Assumption that x = 0 is left, and y = 0 is the top.

        // Make board information
        int boardWidth = GetWidth();
        int boardHeight = GetHeight();

        JewelKind[,] jewelBoard = new JewelKind[boardWidth, boardHeight];

        // Note: X axis first, y axis second
        for (int y = 0; y < boardHeight; y++)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                jewelBoard[x, y] = GetJewel(x, y);
            }
        }

        int currentHighestPossiblePoints = 0;
        Move bestMove = new Move();
        Array directions = Enum.GetValues(typeof(MoveDirection));
        
        // Check all gems in board
        for (int y = 0; y < boardHeight; y++)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                // Check all directions gem can take
                foreach (MoveDirection direction in directions)
                {
                    // If we're trying to move towards board border, skip direction
                    bool leftCheck = (direction == MoveDirection.Left && x == 0);
                    bool rightCheck = (direction == MoveDirection.Right && x == boardWidth-1);
                    bool upCheck = (direction == MoveDirection.Up && y == 0);
                    bool downCheck = (direction == MoveDirection.Down && x == boardHeight-1);
                    if (leftCheck || rightCheck || upCheck || downCheck)
                    {
                        continue;
                    }

                    // Since gem could be moved in this direction, check chain of gems for this Move
                    Move currentMove = new Move();
                    currentMove.x = x;
                    currentMove.y = y;
                    currentMove.direction = direction;
                    int connectedGemCount = GetPointsFromProjectedMove(currentMove,jewelBoard);

                    // If points are higher, make new best move
                    if (connectedGemCount > currentHighestPossiblePoints)
                    {
                        currentHighestPossiblePoints = connectedGemCount;
                        bestMove.x = x;
                        bestMove.y = y;
                        bestMove.direction = direction;
                    }
                }
            }
        }

        return new Move();
    }

    int GetPointsFromProjectedMove(Move moveToExecute,  JewelKind[,] jewelBoard)
    {
        int totalPoints = 1;
        int boardWidth = GetWidth();
        int boardHeight = GetHeight();
        JewelKind desiredJewel = jewelBoard[moveToExecute.x, moveToExecute.y];

        // Setup first node after moving gem
        Vector2Int startPosition = NewPositionAfterMove(moveToExecute);
        MoveDirection backwardsDirection = GetOppositeDirection(moveToExecute.direction);
        KeyValuePair<Vector2Int, MoveDirection> currentNode = new KeyValuePair<Vector2Int, MoveDirection>(startPosition, backwardsDirection);

        // Using a Queue and Set of to search and visited nodes
        Queue<KeyValuePair<Vector2Int, MoveDirection>> searchQueue = new Queue<KeyValuePair<Vector2Int, MoveDirection>>();
        HashSet<Vector2Int> visitedNodes = new HashSet<Vector2Int>();
        searchQueue.Enqueue(currentNode);

        // Breadth first tree search
        foreach (KeyValuePair<Vector2Int, MoveDirection> node in searchQueue)
        {
            Array directions = Enum.GetValues(typeof(MoveDirection));
            int x = node.Key.x;
            int y = node.Key.y;

            foreach (MoveDirection direction in directions)
            {
                //Don't move in direction travelled from
                MoveDirection directionTravelled = node.Value;
                if (direction == directionTravelled)
                {
                    continue;
                }
                
                // If we're trying to move towards board border, skip direction
                bool leftCheck = (direction == MoveDirection.Left && x == 0);
                bool rightCheck = (direction == MoveDirection.Right && x == boardWidth - 1);
                bool upCheck = (direction == MoveDirection.Up && y == 0);
                bool downCheck = (direction == MoveDirection.Down && x == boardHeight - 1);
                if (leftCheck || rightCheck || upCheck || downCheck)
                {
                    continue;
                }

                // Check connected gem is of same type as root
                Vector2Int positionOfGemToCheck = NewPositionAfterMove(direction, x, y);
                if (jewelBoard[positionOfGemToCheck.x, positionOfGemToCheck.y] == desiredJewel)
                {
                    // Add gem to queue if not already searched
                    if (visitedNodes.Add(positionOfGemToCheck))
                    {
                        KeyValuePair<Vector2Int , MoveDirection> newNode = new KeyValuePair<Vector2Int, MoveDirection> (position, GetOppositeDirection(direction));
                        searchQueue.Enqueue(newNode);
                        totalPoints++;
                    }
                }
            }
            searchQueue.Dequeue();
        }
        return totalPoints;
    }

    MoveDirection GetOppositeDirection(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Left:
                return MoveDirection.Right;
            case MoveDirection.Right:
                return MoveDirection.Left;
            case MoveDirection.Up:
                return MoveDirection.Down;
            case MoveDirection.Down:
                return MoveDirection.Up;
            default:
                throw new ArgumentException("Invalid MoveDirection given");
        }
    }

    Vector2Int NewPositionAfterMove(Move move)
    {
        Vector2Int position = new Vector2Int(move.x, move.y);
        switch (move.direction)
        {
            case MoveDirection.Left:
                position.x--;
                return position;
            case MoveDirection.Right:
                position.x++;
                return position;
            case MoveDirection.Up:
                position.y--;
                return position;
            case MoveDirection.Down:
                position.y++;
                return position;
            default:
                throw new ArgumentException("Invalid MoveDirection given");
        }
    }

    Vector2Int NewPositionAfterMove(MoveDirection direction, int x, int y)
    {
        Vector2Int position = new Vector2Int(x, y);
        switch (direction)
        {
            case MoveDirection.Left:
                position.x--;
                return position;
            case MoveDirection.Right:
                position.x++;
                return position;
            case MoveDirection.Up:
                position.y--;
                return position;
            case MoveDirection.Down:
                position.y++;
                return position;
            default:
                throw new ArgumentException("Invalid MoveDirection given");
        }
    }
}