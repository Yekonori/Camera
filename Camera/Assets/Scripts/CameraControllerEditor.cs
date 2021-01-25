using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraControllerEditor : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] float timer = 0f;
    public List<CameraConfiguration> listConfig;
    private Camera myCam;

    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CameraConfiguration bla = CameraConfiguration.ListInterpolation(timer, listConfig);

        Quaternion orientation = Quaternion.Euler(bla.pitch, bla.yaw, bla.roll);
        myCam.transform.rotation = orientation;

        Vector3 offset = orientation * Vector3.back * bla.distance;
        myCam.transform.position = bla.pivot + offset;
        myCam.fieldOfView = bla.fieldOfView;
    }
}
