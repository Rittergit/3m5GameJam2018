using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    public Rigidbody en;
    public Vector3 actualspeed, actualpos;
    private Vector3 pos1, pos2, pos3, pos4, Target, speedvec;
    private float speed;
    int zufall;
    int zu;

    // Use this for initialization


    void Start ()
    {
        pos1 = new Vector3(49, 2, -49);
        pos2 = new Vector3(49, 2, 49);
        pos3 = new Vector3(-49, 2, -49);
        pos4 = new Vector3(-49, 2, 49);
        zu = Random.Range(1, 4);
        switch (zu)
        {
            case 1:
                en.position = pos1;
                break;
            case 2:
                en.position = pos2;
                break;
            case 3:
                en.position = pos3;
                break;
            case 4:
                en.position = pos4;                
                break;
        }



        actualspeed = new Vector3();
        actualpos = new Vector3();
        speedvec = new Vector3();
    }
	
	// Update is called once per frame
	void Update () {
        Target = new Vector3(0, 0, 0);
        en.GetComponent<Rigidbody>();
        actualpos = en.position;
        actualspeed = en.velocity;
        

        speedvec = actualpos - Target;
        speed = Mathf.Sqrt(speedvec.x * speedvec.x + speedvec.y *speedvec.y);
        en.velocity = new Vector3(-actualpos.x / speed , actualspeed.y, -actualpos.z/speed);

    }
}
