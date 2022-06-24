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
    
    public static Quaternion EulerToQuaternion(this Vector3 euler)
    {
        float xOver2 = euler.x * Mathf.Deg2Rad * 0.5f;
        float yOver2 = euler.y * Mathf.Deg2Rad * 0.5f;
        float zOver2 = euler.z * Mathf.Deg2Rad * 0.5f;

        float sinXOver2 = Mathf.Sin(xOver2);
        float cosXOver2 = Mathf.Cos(xOver2);
        float sinYOver2 = Mathf.Sin(yOver2);
        float cosYOver2 = Mathf.Cos(yOver2);
        float sinZOver2 = Mathf.Sin(zOver2);
        float cosZOver2 = Mathf.Cos(zOver2);

        UnityEngine.Quaternion result;
        result.x = cosYOver2 * sinXOver2 * cosZOver2 + sinYOver2 * cosXOver2 * sinZOver2;
        result.y = sinYOver2 * cosXOver2 * cosZOver2 - cosYOver2 * sinXOver2 * sinZOver2;
        result.z = cosYOver2 * cosXOver2 * sinZOver2 - sinYOver2 * sinXOver2 * cosZOver2;
        result.w = cosYOver2 * cosXOver2 * cosZOver2 + sinYOver2 * sinXOver2 * sinZOver2;

        return result;
    }
}