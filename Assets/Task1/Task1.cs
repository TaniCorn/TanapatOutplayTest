using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1
{

    bool TryCalculateXPositionAtHeight(float h, Vector2 p, Vector2 v, float G, float w, ref float xPosition)
    {
        //See ReadME, G Issue. In short, no assumption is made that G will be negative.
        if (G > 0)
        {
            G *= -1;
        }

        // Greatest Height Attained formula
        // V^2 * Sin^2 * a / 2g , where 'a' is the angle between the horizontal axis
        // 'a' can be figured out from the dot product
        Vector2 horizontal = Vector2.right;
        float angle = Vector2.Angle(horizontal, v);

        float maximumHeight = ((v.y * v.y) * (Mathf.Sin(angle) * Mathf.Sin(angle))) / 2 * G;

        // If initial position is higher than 'h' already, or initial position and max height is higher than 'h',
        // continue calculations, otherwise return false
        if (!(p.y > h || p.y + maximumHeight > h))
        {
            return false;
        }

        // Imagining intial point now exists at y=0 and h gets shifted down the same amount
        float yDisplacement = h - p.y;
        // s = u*t + 1/2 * a * t^2, Re-arranged this formula for time
        // Get the time the ball reaches height h
        float t = (-v.y - Mathf.Sqrt(v.y * v.y + 2 * G * yDisplacement)) / G; 

        // s = u*t, How far along the x-axis have we moved when time = t
        // Displacement relative to x = 0
        float xDisplacement = (v.x * t) + p.x;

        // Determining the amount of times we've bounced and which wall we bounce on
        // bounces < 0 then the left wall was the first wall, bounces > 0 then the right wall was the first wall
        int bounces = Mathf.FloorToInt(xDisplacement / w);
        if (bounces == 0)
        {
            xPosition = xDisplacement;
            return true;
        }

        // Bounce on right wall 5 - 5.4 = -0.4
        // Bounce on left wall -5 - -0.4 = -4.6
        // Getting the displacement relative to x = w, should always be negative and less than w
        float scaledDisplacement = (w * bounces - xDisplacement);

        xPosition = w + scaledDisplacement;
        return true;

    }
}