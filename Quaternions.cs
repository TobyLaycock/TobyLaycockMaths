using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quaternions {

  
    public static Vector3 RotateVertexAroundAxis(float Angle, Vector3 Axis, Vector3 Vertex)
    {
        Vector3 rv = (Vertex * Mathf.Cos(Angle)) +
            Maths.DotProduct(Vertex, Axis) * Axis * (1 - Mathf.Cos(Angle)) +
            Maths.CrossProduct(Axis, Vertex) * Mathf.Sin(Angle);

        return rv;
    }

    public class Quat
    {
        public float w, x, y, z;
        

        public Quat()
        {

        }
        public Quat(float Angle, Vector3 Axis)
        {
            float halfAngle = Angle / 2;
            w = Mathf.Cos(halfAngle);
            x = Axis.x * Mathf.Sin(halfAngle);
            y = Axis.y * Mathf.Sin(halfAngle);
            z = Axis.z * Mathf.Sin(halfAngle);
        }

        public Quat(Vector3 Axis)
        {
            x = Axis.x;
            y = Axis.y;
            z = Axis.z;
            w = 0;
        }
   

        public void vectorSet(Vector3 vect)
        {
            x = vect.x;
            y = vect.y;
            z = vect.z;
        }
        public Vector3 vectorGet()
        {
            Vector3 rv = new Vector3(x, y, z);
            return rv;
        }
        public static Quat operator *(Quat rhs, Quat lhs)
        {
            Quat rv = new Quat();
            rv.w = lhs.w * rhs.w - Maths.DotProduct(lhs.getAxis(), rhs.getAxis());
            rv.vectorSet((lhs.w * rhs.getAxis()) + rhs.w * lhs.getAxis() +(Maths.CrossProduct(rhs.getAxis(), lhs.getAxis())));
            return rv;
        }

        public Vector3 getAxis()
        {
            return new Vector3(x, y, z);
        }

        public void setAxis(Vector3 rv)
        {
            x = rv.x;
            y = rv.y;
            z = rv.z;
        }
        public Quat Inverse()
        {
            Quat rv = new Quat();
            rv.w = w;
            rv.setAxis(-getAxis());
            return rv;
        }

        public Vector4 GetAxisAngle()
        {
            Vector4 rv = new Vector4();
            float halfAngle = Mathf.Acos(w);
            rv.w = halfAngle * 2;

            rv.x = x / Mathf.Sin(halfAngle);
            rv.y = y / Mathf.Sin(halfAngle);
            rv.z = z / Mathf.Sin(halfAngle);

            return rv;
        }

        public static Quat SLERP(Quat q, Quat r, float t)
        {
            t = Mathf.Clamp(t, 0.0f, 1.0f);
            Quat d = r * q.Inverse();
            Vector4 AxisAngle = d.GetAxisAngle();
            Quat dT = new Quat(AxisAngle.w * t, new Vector3(AxisAngle.x, AxisAngle.y, AxisAngle.z));

            return dT * q;
        }

        
    }
}
