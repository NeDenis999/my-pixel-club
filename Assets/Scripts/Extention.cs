using UnityEngine;

public static class Extention
{
    public static void SetAlpha(this Color color, float alpha)
    {
        color.a = alpha;
    }
}