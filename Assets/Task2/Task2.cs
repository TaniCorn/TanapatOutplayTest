using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Search;
using UnityEngine;

public class Board
{
    const int connectedGemsRequiredToMatch = 3;

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
        // Note: for loops have X axis traversed first, y axis second
        Move bestMove = new Move();
        Array directions = Enum.GetValues(typeof(MoveDirection));
        int boardWidth = GetWidth();
        int boardHeight = GetHeight();
        JewelKind[,] jewelBoard = new JewelKind[boardWidth, boardHeight];

        // Make board information
        for (int y = 0; y < boardHeight; y++)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                jewelBoard[x, y] = GetJewel(x, y);
            }
        }

        int currentHighestPossiblePoints = 0;

        // Check all gems in board
        for (int y = 0; y < boardHeight; y++)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                Move currentMove;
                currentMove.x = x;
                currentMove.y = y;

                // Check all directions gem can take
                foreach (MoveDirection direction in directions)
                {
                    currentMove.direction = direction;

                    if (IsMoveOutOfBounds(direction, currentMove.x, currentMove.y, boardWidth, boardHeight))
                    {
                        continue;
                    }

                    // Get points gained from move, If points are higher, make new best move
                    int totalPointsFromMove = GetPointsFromProjectedMove(currentMove,jewelBoard);
                    if (totalPointsFromMove > currentHighestPossiblePoints)
                    {
                        currentHighestPossiblePoints = totalPointsFromMove;
                        bestMove.x = x;
                        bestMove.y = y;
                        bestMove.direction = direction;
                    }
                }
            }
        }

        return bestMove;
    }

    /// <summary>
    /// Get overall points from move, does not actually execute the move.
    /// </summary>
    int GetPointsFromProjectedMove(Move primaryMove,  JewelKind[,] jewelBoard)
    {
        // Function will check points gained from primary move, and then check points gained from the gem swapped with primary move
        // Does not actually move the gem, function pretends gem is moved, and just doesn't check direction it came from

        Vector2Int otherGemPosition = NewPositionAfterMove(primaryMove);
        Move secondaryMove;
        secondaryMove.x = otherGemPosition.x;
        secondaryMove.y = otherGemPosition.y;
        secondaryMove.direction = GetOppositeDirection(primaryMove.direction);

        // Get points from primary gem and secondary gem movement
        int connectedGemCount = GetPointsFromConnectedGem(primaryMove, jewelBoard);
        int otherGemCount = GetPointsFromConnectedGem(secondaryMove, jewelBoard);

        // Add gems if they have made a valid connection
        int totalPointsGainedFromMove = 0;
        totalPointsGainedFromMove += otherGemCount >= connectedGemsRequiredToMatch ? otherGemCount : 0;
        totalPointsGainedFromMove += connectedGemCount >= connectedGemsRequiredToMatch ? connectedGemCount : 0;

        return totalPointsGainedFromMove;
    }

    /// <summary>
    /// Checks connected gems from the 1 gem that is moving, and returns as points. Does not actually move gem.
    /// </summary>
    int GetPointsFromConnectedGem(Move moveToExecute, JewelKind[,] jewelBoard)
    {
        int totalPoints = 1;
        int boardWidth = GetWidth();
        int boardHeight = GetHeight();
        Array directions = Enum.GetValues(typeof(MoveDirection));
        JewelKind gemKind = jewelBoard[moveToExecute.x, moveToExecute.y];

        // Setup first node after moving gem
        Vector2Int startPosition = NewPositionAfterMove(moveToExecute);
        MoveDirection backwardsDirection = GetOppositeDirection(moveToExecute.direction);
        KeyValuePair<Vector2Int, MoveDirection> currentNode = new KeyValuePair<Vector2Int, MoveDirection>(startPosition, backwardsDirection);

        // Using a Queue and Set of to search and visited nodes
        Queue<KeyValuePair<Vector2Int, MoveDirection>> searchQueue = new Queue<KeyValuePair<Vector2Int, MoveDirection>>();
        HashSet<Vector2Int> visitedNodes = new HashSet<Vector2Int>();
        searchQueue.Enqueue(currentNode);
        visitedNodes.Add(startPosition);

        // Breadth first tree search
        while (searchQueue.Count != 0)
        {
            KeyValuePair<Vector2Int, MoveDirection> node = searchQueue.Dequeue();
            Move currentMove;
            currentMove.x = node.Key.x;
            currentMove.y = node.Key.y;

            foreach (MoveDirection direction in directions)
            {
                currentMove.direction = node.Value;

                if (direction == currentMove.direction || IsMoveOutOfBounds(direction, currentMove.x, currentMove.y, boardWidth, boardHeight))
                {
                    continue;
                }

                // Check connected gem is of same type as root
                Vector2Int positionOfGemToCheck = NewPositionAfterMove(direction, currentMove.x, currentMove.y);
                if (jewelBoard[positionOfGemToCheck.x, positionOfGemToCheck.y] == gemKind)
                {
                    // Add gem to queue if not already searched
                    if (visitedNodes.Add(positionOfGemToCheck))
                    {
                        KeyValuePair<Vector2Int, MoveDirection> newNode = new KeyValuePair<Vector2Int, MoveDirection>(positionOfGemToCheck, GetOppositeDirection(direction));
                        searchQueue.Enqueue(newNode);
                        totalPoints++;
                    }
                }
            }
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

    bool IsMoveOutOfBounds(MoveDirection direction, int x, int y, int boardWidth, int boardHeight)
    {
        bool leftCheck = (direction == MoveDirection.Left && x == 0);
        bool rightCheck = (direction == MoveDirection.Right && x == boardWidth - 1);
        bool upCheck = (direction == MoveDirection.Up && y == 0);
        bool downCheck = (direction == MoveDirection.Down && y == boardHeight - 1);
        return (leftCheck || rightCheck || upCheck || downCheck);
    }
}