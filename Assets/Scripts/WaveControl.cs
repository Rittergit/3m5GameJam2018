using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveControl : MonoBehaviour {

    public GameObject enemy;
    float waitTime = 10.0f;//2Sekunden warten als standart Wert
    float elapsedTime = 0.0f;
    string levelName = "";
    float zeroTime;


	// Use this for initialization
	void Start () {
        enemy.GetComponent<enemy>();
        zeroTime = Time.realtimeSinceStartup;
        
	}
    // Update is called once per frame
    void Update ()
    {
        elapsedTime = Time.realtimeSinceStartup;
        if (elapsedTime >= waitTime)
        {
            Instantiate(enemy);
            waitTime += 1;
        }
    }
}


