using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    public static void RemoveDuplicateValues<T>(ref List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < list.Count; j++)
            {
                if(i != j && list[i].Equals(list[j]))
                {
                    list.RemoveAt(j);
                }
            }
        }
    }
}
