using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utilities
{
    public static bool InRange(Vector2Int range, int value)
    {
        return range.x <= value && value <= range.y;
    }
    public static bool InRange(Vector2 range, float value)
    {
        return range.x <= value && value <= range.y;
    }

    public static T GetRandomElement<T>(this ICollection<T> collection, System.Random rd)
    {
        int index = rd.Next(0, collection.Count);
        return collection.ToList().ElementAt(index);
    }

    public static T GetRandomElement<T>(this ICollection<T> collection)
    {
        int index = UnityEngine.Random.Range(0, collection.Count);
        return collection.ToList().ElementAt(index);
    }
}
