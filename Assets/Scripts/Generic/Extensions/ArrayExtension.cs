using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public static class ArrayExtension
{
    public static T GetRandom<T>(this IList<T> array)
    {
        var index = Random.Range(0, array.Count());

        return array[index];
    }
}
