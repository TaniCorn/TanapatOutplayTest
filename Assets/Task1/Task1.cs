using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : MonoBehaviour
{

    public void Start()
    {
        float gravity = -9.8f;
        float xPosition = 0;
        //Testing if it can reach height
        bool success = TryCalculateXPositionAtHeight(10.0f, new Vector2(3, 6), new Vector2(0, 2), gravity, 5, ref xPosition);
        Debug.Log("Test 1H: " +  success + " with xPosition: " + xPosition);
        
        //Testing left movement
        success = TryCalculateXPositionAtHeight(1.0f, new Vector2(3, 6), new Vector2(-1, 0), gravity, 5, ref xPosition);
        Debug.Log("Test 2LM: " +  success + " with xPosition: " + xPosition);

        //Testing right movement
        success = TryCalculateXPositionAtHeight(1.0f, new Vector2(3, 6), new Vector2(1, 0), gravity, 5, ref xPosition);
        Debug.Log("Test 3RM: " + success + " with xPosition: " + xPosition);

        //Testing left bounce
        success = TryCalculateXPositionAtHeight(1.0f, new Vector2(0.1f, 6), new Vector2(-1, 0), gravity, 5, ref xPosition);
        Debug.Log("Test 4LB: " + success + " with xPosition: " + xPosition);

        //Testing right bounce
        success = TryCalculateXPositionAtHeight(1.0f, new Vector2(4.9f, 6), new Vector2(1, 0), gravity, 5, ref xPosition);
        Debug.Log("Test 5RB: " + success + " with xPosition: " + xPosition);
    }


    bool TryCalculateXPositionAtHeight(float h, Vector2 p, Vector2 v, float G, float w, ref float xPosition)
    {
        //See ReadME, G Issue. In short, no assumption is made that G will be negative.
        if(G > 0)
        {
            G *= -1;
        }

        // Greatest Height Attained formula
        // V^2 * Sin^2 * a / 2g , where 'a' is the angle between the horizontal axis
        // 'a' can be figured out from the dot product
        Vector2 horizontal = Vector2.right;
        float angle = Vector2.Angle(horizontal, v);

        float maximumHeight = ( (v.y * v.y) * (Mathf.Sin(angle) * Mathf.Sin(angle)) ) / 2 * G;

        // If initial position is higher than 'h' already, or initial position and max height is higher than 'h',
        // continue calculations, otherwise return false
        if (!(p.y > h || p.y + maximumHeight > h))
        {
            return false;
        }

        // s = u*t + 1/2 * a * t^2
        // Re-arranged formula for time
        float displacement = h - p.y; // Imagining intial point now exists at y=0 and h gets shifted down the same amount
        float t = (-v.y + Mathf.Sqrt(v.y*v.y + 2*G*displacement)) / G; // Get the time we hit h

        // How far along the x-axis have we moved when time = t
        // s = u*t
        float xDisplacement = v.x * t;

        // Determining the amount of times we've bounced and which wall we bounce on
        int bounces = Mathf.FloorToInt((xDisplacement + p.x / w));
        float finalBounceDistance = xDisplacement - w*bounces;

        int reflectionSign = bounces % 2;
        //If we have a reflection sign of 1, then we are in the reflected space (We have bounced)
        if (reflectionSign == 0)
        {
            xPosition = finalBounceDistance;
        }
        else
        {
            xPosition = w - finalBounceDistance;
        }

        return true;
    }

}
