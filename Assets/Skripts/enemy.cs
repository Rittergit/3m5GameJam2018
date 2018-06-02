using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    public Rigidbody phys;
	// Use this for initialization
	void Start () {
        phys = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        phys.velocity = new Vector3(1, 0, 0);
	}
}
