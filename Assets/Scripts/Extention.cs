using UnityEngine;

public static class Extention
{
    public static void SetAlpha(this Color color, float alpha) => 
        color.a = alpha;

    public static Vector2 RandomVector2(this Vector3 vector, float maxDistance) => 
        (Vector2)vector + new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));

    public static Vector3 ToY(this Vector3 vector, float y)
    {
        Vector3 vector3 = vector;
        vector3.y = y;
        return vector3;
    }
}