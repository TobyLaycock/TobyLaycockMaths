using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaternionBehaviour : MonoBehaviour {
    public float angle;
    public float one;
    public float two;
    public float three;
    public float speed;
    float a;
    public Vector3 rotAngle;
    float yawAngle;
    float scale;
    float rotationAngle = 30.0f;
    float rotationSpeed = 70.0f;
    Vector3[] ModelSpaceVertices;


    public Vector3 position;
    
    public Vector3 angle2;
    MeshFilter MF;


    // Use this for initialization
    void Start () {
        
        scale = 1;
        MeshFilter MF = GetComponent<MeshFilter>();
        ModelSpaceVertices = MF.mesh.vertices;
        rotAngle = new Vector3(50, 0, 0);
        yawAngle = 70.0f;
    }
	
	// Update is called once per frame
	void Update () {

        orbit();
        rotate();


    }


    //Code for Orbiting around Sun
    void orbit()
    {
        angle += Time.deltaTime * speed;
        a += Time.deltaTime * 0.5f;



        Quaternions.Quat q = new Quaternions.Quat(angle, new Vector3(0, 1, 0));
        Vector3 p = new Vector3(one, two, three);
        Quaternions.Quat K = new Quaternions.Quat(1.0f, p);
        Quaternions.Quat newK = q * K * q.Inverse();
        Vector3 newP = newK.getAxis();


        transform.position = newP;
    }

    //Code for Axis Rotation 
   void rotate()
    {
        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];
        yawAngle += Time.deltaTime * speed;

        Matrix4by4 rollMatrix = new Matrix4by4(
            new Vector3(Mathf.Cos(0.0f), Mathf.Sin(0.0f), 0),
            new Vector3(-Mathf.Sin(0.0f), Mathf.Cos(0.0f), 0),
            new Vector3(0, 0, 1),
            Vector3.zero);

        Matrix4by4 pitchMatrix = new Matrix4by4(
            new Vector3(1, 0, 0),
            new Vector3(0, Mathf.Cos(yawAngle), Mathf.Sin(yawAngle)),
            new Vector3(0, -Mathf.Sin(yawAngle), Mathf.Cos(yawAngle)),
            Vector3.zero);

        Matrix4by4 yawMatrix = new Matrix4by4(
            new Vector3(Mathf.Cos(0.0f), 0, -Mathf.Sin(0.0f)),
            new Vector3(0, 1, 0),
            new Vector3(Mathf.Sin(0.0f), 0, Mathf.Cos(0.0f)),
            Vector3.zero);

        Matrix4by4 R = yawMatrix * (pitchMatrix * rollMatrix);

        for (int i = 0; i < TransformedVertices.Length; i++)
        {
            TransformedVertices[i] = R * ModelSpaceVertices[i];
        }

        MeshFilter MF = GetComponent<MeshFilter>();
        MF.mesh.vertices = TransformedVertices;
        MF.mesh.RecalculateNormals();
        MF.mesh.RecalculateBounds();
    }
}
        




