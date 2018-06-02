using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    public int speed;
    public Rigidbody en;
    private Vector3 actualspeed;
	// Use this for initialization


	void Start ()
    {
        en.position = new Vector3(0, 20, 0);
    }
	
	// Update is called once per frame
	void Update () {
        actualspeed = new Vector3();
        en.GetComponent<Rigidbody>();
        en.transform.TransformPoint(actualspeed);
        en.velocity = new Vector3(10, actualspeed.y, actualspeed.z);
    }
}
