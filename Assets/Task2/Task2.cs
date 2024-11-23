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
        // Implement this function


        return new Move();
    }
}