using UnityEngine;

public static class Extention
{
    public static void SetAlpha(this Color color, float alpha) => 
        color.a = alpha;

    public static Vector2 RandomVector2(this Vector3 vector, float maxDistance) => 
        (Vector2)vector + new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));

    public static Vector3 ToX(this Vector3 vector, float x)
    {
        Vector3 vector3 = vector;
        vector3.x = x;
        return vector3;
    }

    public static Vector3 ToY(this Vector3 vector, float y)
    {
        Vector3 vector3 = vector;
        vector3.y = y;
        return vector3;
    }
    
    public static Vector3 ToZ(this Vector3 vector, float z)
    {
        Vector3 vector3 = vector;
        vector3.z = z;
        return vector3;
    }

    public static Vector3 ToMove(this Vector3 vector, Vector3 direction)
    {
        Vector3 vector3 = vector;
        vector3 += direction;
        return vector3;
    }
}