using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController2 : MonoBehaviour {

    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;

    public float scrollSpeed = 20;
    public float minY = 5f;
    public float maxY = 60;

    public float rotateH = 2;
    public float rotateV = 2;
    public float minPitch = 10;
    public float maxPitch = 80;
    float yaw = 0, pitch = 0;

    public float yPosition = 5;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            // pos.z += panSpeed * Time.deltaTime;
            transform.Translate(Vector3.up * Time.deltaTime * panSpeed);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(-Vector3.up * Time.deltaTime * panSpeed);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * Time.deltaTime * panSpeed);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * Time.deltaTime * panSpeed);
        }

      
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y = Mathf.Clamp(pos.y,minY,maxY);
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;
        if (pos.y <= 5) pos.y = 5;
        if (pos.y >= 60) pos.y = 60;
        yPosition = pos.y;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        /// Rotation with mouse
        pitch = transform.eulerAngles.x;
        yaw = transform.eulerAngles.y;

        if (Input.GetMouseButton(2)) // Right mouse button down
        {
            yaw += rotateH * Input.GetAxis("Mouse X");
            pitch -= rotateV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(Mathf.Clamp(pitch, minPitch, maxPitch), yaw, 0.0f);
        }
      
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
        
    }
}
