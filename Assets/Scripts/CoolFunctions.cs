using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoolFunctions
{
    public static float MapValues(float value, float leftMin, float leftMax, float rightMin, float rightMax)
    {
        return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
    }

    public static void SwapValues<T>(ref T A, ref T B)
    {
        T temp = A;
        B = A;
        A = temp;
    }
}
