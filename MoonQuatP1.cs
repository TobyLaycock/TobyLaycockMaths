using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonQuatP1 : MonoBehaviour
{


    public float a;
    public float angle;
    public float one;
    public float two;
    public float three;
    public float speed;
    GameObject planet;
    public string planetNum;   

    
   
    void Start()
    {
        //Sets the variable planet to the planet that is chosen (planetNum string)
        planet = GameObject.Find(planetNum);
    }

   
    void Update()
    {
        rotatemoon();
    }


    //Rotation of the moon
    void rotatemoon()
    {
        angle += Time.deltaTime * speed;

       //Saves location of planet in a vector 3
        Vector3 planetPos = planet.transform.position;



        Quaternions.Quat q = new Quaternions.Quat(angle, new Vector3(0, 1, 0));
        Vector3 p = new Vector3(one, two, three);
        Quaternions.Quat K = new Quaternions.Quat(1.0f, p);
        Quaternions.Quat newK = q * K * q.Inverse();
        Vector3 newP = newK.getAxis();

        //Sets the location for the orbit to the planet thats chosen
        transform.position = newP + planetPos;
    }
}
