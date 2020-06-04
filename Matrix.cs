using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix : MonoBehaviour
{ 

    Vector3[] ModelSpaceVertices;
   
    public Vector3 position;
    public Vector3 scale = new Vector3 (1, 1, 1);
    public Vector3 angle;
    MeshFilter MF;
    
    void Start()
    {
       
     
    }

    void Update () {

        MF = GetComponent<MeshFilter>();
        ModelSpaceVertices = MF.mesh.vertices; 

        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];
        
        

         Matrix4by4 rollMatrix = new Matrix4by4(
          new Vector3(Mathf.Cos(angle.z), Mathf.Sin(angle.z), 0),
          new Vector3(-Mathf.Sin(angle.z), Mathf.Cos(angle.z), 0),
          new Vector3(0, 0, 1),
          Vector3.zero);

        Matrix4by4 pitchMatrix = new Matrix4by4(
            new Vector3(1, 0, 0),
            new Vector3(0, Mathf.Cos(angle.x), Mathf.Sin(angle.x)),
            new Vector3(0, -Mathf.Sin(angle.x), Mathf.Cos(angle.x)),
            Vector3.zero);

        Matrix4by4 yawMatrix = new Matrix4by4(
            new Vector3(Mathf.Cos(angle.y), 0, -Mathf.Sin(angle.y)),
            new Vector3(0, 1, 0),
            new Vector3(Mathf.Sin(angle.y), 0, Mathf.Cos(angle.y)),
            Vector3.zero);

        Matrix4by4 S = new Matrix4by4(new Vector3(1, 0, 0) * scale.x, new Vector3(0, 1, 0) * scale.y, new Vector3(0, 0, 1) * scale.z, Vector3.zero);

        Matrix4by4 R = yawMatrix * (pitchMatrix * rollMatrix);

        Matrix4by4 T = new Matrix4by4(
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1),
            new Vector3(position.x, position.y, position.y));

        Matrix4by4 M = T * (R * S);
     
      for (int i = 0; i < TransformedVertices.Length; i++)
        {
            TransformedVertices[i] = M * ModelSpaceVertices[i];
        }


        MF = GetComponent<MeshFilter>();
        MF.mesh.vertices = TransformedVertices;

        MF.mesh.RecalculateNormals();
        

       
    }
}

public class Matrix4by4
{
   
    public Matrix4by4(Vector4 column1, Vector4 column2, Vector4 column3, Vector4 column4)
    {
        values = new float[4, 4];

        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = column1.w;

        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = column2.w;

        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = column3.w;

        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = column4.w;

    }

    public Matrix4by4(Vector3 column1, Vector3 column2, Vector3 column3, Vector3 column4)
    {
        values = new float[4, 4];

        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = 0;

        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = 0;

        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = 0;

        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = 1;
    }

    public Matrix4by4() { values = new float[4, 4]; }

    public float[,] values;

    public static Vector4 operator *(Matrix4by4 lhs, Vector4 vector)
    {
        vector.w = 1;
        Vector4 rv = new Vector4();
        rv.x = (lhs.values[0, 0] * vector.x) + (lhs.values[0, 1] * vector.y) + (lhs.values[0, 2] * vector.z) + (lhs.values[0, 3] * vector.w);
        rv.y = (lhs.values[1, 0] * vector.x) + (lhs.values[1, 1] * vector.y) + (lhs.values[1, 2] * vector.z) + (lhs.values[1, 3] * vector.w);
        rv.z = (lhs.values[2, 0] * vector.x) + (lhs.values[2, 1] * vector.y) + (lhs.values[2, 2] * vector.z) + (lhs.values[2, 3] * vector.w);
        rv.w = (lhs.values[3, 0] * vector.x) + (lhs.values[3, 1] * vector.y) + (lhs.values[3, 2] * vector.z) + (lhs.values[3, 3] * vector.w);

        return rv;
    }


   

    public static Matrix4by4 Identity
    {
      get
      {
            return new Matrix4by4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 0, 0, 1));
      }
    }
    public static Matrix4by4 operator *(Matrix4by4 vector1, Matrix4by4 vector2)
    {
        Matrix4by4 vecResult = new Matrix4by4();

        vecResult.values[0, 0] = vector1.values[0, 0] * vector2.values[0, 0] + vector1.values[0, 1] * vector2.values[1, 0] + vector1.values[0, 2] * vector2.values[2, 0] + vector1.values[0, 3] * vector2.values[3, 0];
        vecResult.values[0, 1] = vector1.values[0, 0] * vector2.values[0, 1] + vector1.values[0, 1] * vector2.values[1, 1] + vector1.values[0, 2] * vector2.values[2, 1] + vector1.values[0, 3] * vector2.values[3, 1];
        vecResult.values[0, 2] = vector1.values[0, 0] * vector2.values[0, 2] + vector1.values[0, 1] * vector2.values[1, 2] + vector1.values[0, 2] * vector2.values[2, 2] + vector1.values[0, 3] * vector2.values[3, 2];
        vecResult.values[0, 3] = vector1.values[0, 0] * vector2.values[0, 3] + vector1.values[0, 1] * vector2.values[1, 3] + vector1.values[0, 2] * vector2.values[2, 3] + vector1.values[0, 3] * vector2.values[3, 3];

        vecResult.values[1, 0] = vector1.values[1, 0] * vector2.values[0, 0] + vector1.values[1, 1] * vector2.values[1, 0] + vector1.values[1, 2] * vector2.values[2, 0] + vector1.values[1, 3] * vector2.values[3, 0];
        vecResult.values[1, 1] = vector1.values[1, 0] * vector2.values[0, 1] + vector1.values[1, 1] * vector2.values[1, 1] + vector1.values[1, 2] * vector2.values[2, 1] + vector1.values[1, 3] * vector2.values[3, 1];
        vecResult.values[1, 2] = vector1.values[1, 0] * vector2.values[0, 2] + vector1.values[1, 1] * vector2.values[1, 2] + vector1.values[1, 2] * vector2.values[2, 2] + vector1.values[1, 3] * vector2.values[3, 2];
        vecResult.values[1, 3] = vector1.values[1, 0] * vector2.values[0, 3] + vector1.values[1, 1] * vector2.values[1, 3] + vector1.values[1, 2] * vector2.values[2, 3] + vector1.values[1, 3] * vector2.values[3, 3];

        vecResult.values[2, 0] = vector1.values[2, 0] * vector2.values[0, 0] + vector1.values[2, 1] * vector2.values[1, 0] + vector1.values[2, 2] * vector2.values[2, 0] + vector1.values[2, 3] * vector2.values[3, 0];
        vecResult.values[2, 1] = vector1.values[2, 0] * vector2.values[0, 1] + vector1.values[2, 1] * vector2.values[1, 1] + vector1.values[2, 2] * vector2.values[2, 1] + vector1.values[2, 3] * vector2.values[3, 1];
        vecResult.values[2, 2] = vector1.values[2, 0] * vector2.values[0, 2] + vector1.values[2, 1] * vector2.values[1, 2] + vector1.values[2, 2] * vector2.values[2, 2] + vector1.values[2, 3] * vector2.values[3, 2];
        vecResult.values[2, 3] = vector1.values[2, 0] * vector2.values[0, 3] + vector1.values[2, 1] * vector2.values[1, 3] + vector1.values[2, 2] * vector2.values[2, 3] + vector1.values[2, 3] * vector2.values[3, 3];

        vecResult.values[3, 0] = vector1.values[3, 0] * vector2.values[0, 0] + vector1.values[3, 1] * vector2.values[1, 0] + vector1.values[3, 2] * vector2.values[2, 0] + vector1.values[3, 3] * vector2.values[3, 0];
        vecResult.values[3, 1] = vector1.values[3, 0] * vector2.values[0, 1] + vector1.values[3, 1] * vector2.values[1, 1] + vector1.values[3, 2] * vector2.values[2, 1] + vector1.values[3, 3] * vector2.values[3, 1];
        vecResult.values[3, 2] = vector1.values[3, 0] * vector2.values[0, 2] + vector1.values[3, 1] * vector2.values[1, 2] + vector1.values[3, 2] * vector2.values[2, 2] + vector1.values[3, 3] * vector2.values[3, 2];
        vecResult.values[3, 3] = vector1.values[3, 0] * vector2.values[0, 3] + vector1.values[3, 1] * vector2.values[1, 3] + vector1.values[3, 2] * vector2.values[2, 3] + vector1.values[3, 3] * vector2.values[3, 3];

        return vecResult;
    }



}



