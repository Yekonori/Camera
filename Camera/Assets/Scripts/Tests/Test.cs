using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Test : MonoBehaviour
{
    [SerializeField]
    CameraConfiguration cameraConfig;

    Camera myCam;

    private void Start()
    {
        myCam = Camera.main;
    }

    private void Update()
    {
        Quaternion orientation = Quaternion.Euler(cameraConfig.pitch, cameraConfig.yaw, cameraConfig.roll);
        myCam.transform.rotation = orientation;
        Vector3 offset = orientation * (Vector3.back * cameraConfig.distance);
        transform.position = cameraConfig.pivot + offset;
        myCam.fieldOfView = cameraConfig.fieldOfView;
    }
}
