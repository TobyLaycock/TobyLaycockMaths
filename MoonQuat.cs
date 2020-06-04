using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonQuat : MonoBehaviour {


    public float a;
    public float angle;
    public float one;
    public float two;
    public float three;
    public float speed;
    GameObject planet;
    Vector3 planetPos;
    public string planetNum;
    // Use this for initialization
    void Start() {

        planet = GameObject.Find("planet");
    }

    // Update is called once per frame
    void Update() {
        rotatemoon();
    }

    void rotatemoon(){
    angle += Time.deltaTime * speed;
 
    planetPos = planet.transform.position;



        Quaternions.Quat q = new Quaternions.Quat(angle, new Vector3(0, 1, 0));
    Vector3 p = new Vector3(one, two, three);
    Quaternions.Quat K = new Quaternions.Quat(1.0f, p);
    Quaternions.Quat newK = q * K * q.Inverse();
    Vector3 newP = newK.getAxis();


    transform.position = newP + planetPos;
        }
}
