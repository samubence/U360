using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject headObject;
    GameObject cameraContainerObject;

    public float mouseSensitivity = 1;
    Vector3 touchPos;
    bool gyroEnabled;
    Gyroscope gyro;
    Quaternion rot;

    /*
     TODO: attach script to camera
     create CameraContainer bojects
     create Head trasform as well when using mouse
     */

    void Start()
    {
        cameraContainerObject = new GameObject("camera container");
        cameraContainerObject.transform.position = transform.position;

        headObject = new GameObject("camera head");
        headObject.transform.position = transform.position;

        transform.SetParent(headObject.transform);
        headObject.transform.SetParent(cameraContainerObject.transform);

        gyroEnabled = SystemInfo.supportsGyroscope;
        
        if ( gyroEnabled )
        {                    
            gyro = Input.gyro;
            gyro.enabled = true;
            cameraContainerObject.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            rot = new Quaternion(0, 0, 1, 0);
        }        
    }
    
    void Update()
    {
        if (gyroEnabled)
        {
            headObject.transform.localRotation = gyro.attitude * rot;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            touchPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 v = (Input.mousePosition - touchPos) * mouseSensitivity;

            if (gyroEnabled)
            {
                cameraContainerObject.transform.Rotate(0, 0, v.x);
            }
            else
            {
                cameraContainerObject.transform.Rotate(0, -v.x, 0);
                headObject.transform.Rotate(v.y, 0, 0);
            }
            touchPos = Input.mousePosition;
        }
        
    }
}
