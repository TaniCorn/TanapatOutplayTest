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

        JewelKind[,] JewelBoard = new JewelKind[boardWidth, boardHeight];

        // Note: X axis first, y axis second
        for (int y = 0; y < boardHeight; y++)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                JewelBoard[x, y] = GetJewel(x, y);
            }
        }

        int currentHighestPossiblePoints = 0;
        Move bestMove = new Move();
        Array directions = Enum.GetValues(typeof(MoveDirection));
        
        for (int y = 0; y < boardHeight; y++)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                foreach (MoveDirection direction in directions)
                {
                    // Borders check
                    bool leftCheck = (direction == MoveDirection.Left && x == 0);
                    bool rightCheck = (direction == MoveDirection.Right && x == boardWidth-1);
                    bool upCheck = (direction == MoveDirection.Up && y == 0);
                    bool downCheck = (direction == MoveDirection.Down && x == boardHeight-1);

                    if (leftCheck || rightCheck || upCheck || downCheck)
                    {
                        continue;
                    }

                    // Check connected gems 
                    int connectedGemCount = /*Check Connection*/ 0;

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

}