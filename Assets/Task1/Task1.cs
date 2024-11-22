using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1
{
    bool TryCalculateXPositionAtHeight(float h, Vector2 p, Vector2 v, float G, float w, ref float xPosition)
    {

        // Greatest Height Attained formula
        // V^2 * Sin^2 * a / 2g , where 'a' is the angle between the horizontal axis
        // 'a' can be figured out from the dot product
        Vector2 horizontal = Vector2.right;
        float angle = Vector2.Angle(horizontal, v);

        float maximumHeight = ( (v.y * v.y) * (Mathf.Sin(angle) * Mathf.Sin(angle)) ) / 2 * G;

        //If initial position is higher than 'h' already, or initial position and max height is higher than 'h'
        if (p.y > h || p.y + maximumHeight > h)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
