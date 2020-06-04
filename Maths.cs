using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maths { 
    public float x;
    public float y;
    public float z;
    const float pi = 3.14159265359f;



    public static Vector3 VectorNormalized(Vector3 A)
    {
        Vector3 rv = new Vector3(A.x, A.y, A.z);
        rv = rv / VectorLength(rv);
        return rv;
    }

    public static float DotProduct(Vector3 A, Vector3 B)
    {
        float Dp = (A.x * B.x + A.y * B.y + A.z * B.z);

        return Dp;
    }

    public static Vector3 CrossProduct(Vector3 A, Vector3 B)
    {
        Vector3 rv = new Vector3();
        {
            rv.x = (A.y * B.z) - (A.z * B.y);
            rv.y = (A.z * B.x) - (A.x * B.z);
            rv.z = (A.x * B.y) - (A.y * B.x);

            return rv;
        }
    }




    public static float DegtoRad(float v)
    {
        float y = v * pi / 180;
        return y;

    }


    public static Vector3 VectorAdd(Vector3 A, Vector3 B)
    {
        Vector3 rv = new Vector3(0, 0, 0);
        rv.x = (A.x + B.x);
        rv.y = (A.y + B.y);
        rv.z = (A.z + A.z);

        return rv;
    }

    public static Vector3 VectorSubtract(Vector3 A, Vector3 B)
    {
        Vector3 rv = new Vector3(0, 0, 0);
        rv.x = (A.x - B.x);
        rv.y = (A.y - B.y);
        rv.z = (A.z - A.z);
        return rv;
    }

    public static float VectorLength(Vector3 A)
    {
        float rv;

        rv = Mathf.Sqrt((A.x * A.x) + (A.y * A.y) + (A.z * A.z));

        return rv;
    }

    public float LengthSq(Vector3 A)
    {
        float rv;
        rv = VectorLength(A);

        rv = rv * rv;

        return rv;
    }

    public static Vector3 MultiplyVector(Vector3 A, float scalar)
    {
        Vector3 rv = new Vector3(A.x, A.y, A.z);
        rv = rv * scalar;
        return rv;
    }

    public static Vector3 DivideVector(Vector3 A, float divisor)
    {
        Vector3 rv = new Vector3(A.x, A.y, A.z);
        rv = rv / divisor;
        return rv;
    }

   

    public static float DotProductormalize(Vector3 A, Vector3 B, bool Normalize)
    {
        float Dp;
        if (Normalize == true)
        {
            VectorNormalized(A);
            VectorNormalized(B);

        }

        Dp = (A.x * B.x) + (A.y * B.y);

        return Dp;
    }
   

    public static Vector3 MoveTowards(Vector3 A, Vector3 B, float maxDistanceDelta)
    {
        Vector3 rv = B - A;
        float magnitude = rv.magnitude;
        if (magnitude <= maxDistanceDelta || magnitude == 0f)
        {
            return B;
        }

        return A + rv / magnitude * maxDistanceDelta;
    }

    public static float VectorToRadians(Vector2 A)
    {
        float rv = 0.0f;
        rv = Mathf.Atan(A.x / A.y);

        return rv;

    }

    public static Vector2 RadiansToVector(float angle)
    {
        Vector2 rv = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        return rv;
    }

    public static Vector3 EulerAnglesToVector(Vector3 A)
    {
        Vector3 rv = new Vector3();
        rv.x = Mathf.Cos(A.y) * Mathf.Cos(A.x);
        rv.y = Mathf.Sin(A.x);
        rv.z = Mathf.Cos(A.x) * Mathf.Sin(A.y);

        return rv;

    }

    

    public static Vector3 EulerAnglesToDirection(Vector3 EulerAngles)
    {
        Vector3 rv = new Vector3();

        rv.x = Mathf.Cos(EulerAngles.x) * Mathf.Cos(EulerAngles.y);
        rv.y = Mathf.Sin(EulerAngles.x);
        rv.z = Mathf.Cos(EulerAngles.y) * Mathf.Sin(EulerAngles.x);

        return rv;
    }
    public static Vector3 ForwardRotation(Vector3 euler)
    {
        Vector3 Eulerrotation;
        Eulerrotation.x = DegtoRad(euler.x);
        Eulerrotation.y = DegtoRad(euler.y);
        Eulerrotation.z = DegtoRad(euler.z);
        return EulerAnglesToDirection(Eulerrotation);

    }

    public static Vector3 VectorLERP(Vector3 A, Vector3 B, float t)
    {
        return A * (1.0f - t) + B * t;
    }

}
