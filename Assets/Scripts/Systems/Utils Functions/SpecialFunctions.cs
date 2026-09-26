using System;
using System.Collections;

public static class SpecialFunctions
{
    public static IEnumerator DelayMethod(Action method)
    {
        yield return null;
        method?.Invoke();
    }
    public static IEnumerator DelayMethod(Action method, int framesAmount)
    {
        for (int i = 0; i < framesAmount; i++)
            yield return null;
        method?.Invoke();
    }
}
