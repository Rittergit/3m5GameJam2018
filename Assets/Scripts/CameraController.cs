using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float cameraMoveSpeed;
    public float minFov, maxFov;
    public float zoomSensitivity;
    public float rotateH, rotateV;

    public float minPitch, maxPitch;
    float yaw = 0, pitch = 0;

    float yPosition;

    void Start()
    {
        yPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        /// Mouse movement
        if (!Input.GetMouseButton(2)) 
        {
            if (Input.mousePosition.y <= 0)
                transform.position -= transform.forward * cameraMoveSpeed * Time.deltaTime;
            if (Input.mousePosition.y >= Screen.height)
                transform.position += transform.forward * cameraMoveSpeed * Time.deltaTime;

            if (Input.mousePosition.x <= 0)
                transform.Translate(Vector3.left * cameraMoveSpeed * Time.deltaTime, Space.Self);
            if (Input.mousePosition.x >= Screen.width)
                transform.Translate(Vector3.right * cameraMoveSpeed * Time.deltaTime, Space.Self);
        }

        /// Keyboard movement
        transform.position += (transform.forward * cameraMoveSpeed * Time.deltaTime) * Input.GetAxis("Vertical");
        transform.Translate((new Vector3(Input.GetAxis("Horizontal"), 0, 0) * cameraMoveSpeed * Time.deltaTime), Space.Self);

        /// Rotation with mouse
        pitch = transform.eulerAngles.x;
        yaw = transform.eulerAngles.y;

        if (Input.GetMouseButton(2)) // Right mouse button down
        {
            yaw += rotateH * Input.GetAxis("Mouse X");
            pitch -= rotateV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(Mathf.Clamp(pitch, minPitch, maxPitch), yaw, 0.0f); 
        }

        /// Zoom
        float fov = Camera.main.fieldOfView;

        fov -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;

        // Keeps camera at constant height
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
    }
}