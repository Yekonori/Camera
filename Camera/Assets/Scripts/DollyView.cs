using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyView : AView
{
    public float roll = 0f;
    public float distance = 0f;
    public float fov = 0f;

    [SerializeField] Transform target;
    [SerializeField] Rail rail;
    [SerializeField] float speed;
    private Vector3 railPosition = Vector3.zero;

    private float yaw = 0f;
    private float pitch = 0f;

    private float dist = 0f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dist += speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            dist -= speed * Time.deltaTime;
        }
        railPosition = rail.GetPosition(dist);

        Quaternion rot = Quaternion.LookRotation(target.position - railPosition);
        yaw = rot.eulerAngles.y;
        pitch = rot.eulerAngles.x;
    }

    public override CameraConfiguration GetConfiguration()
    {
        return new CameraConfiguration(yaw, pitch, roll, railPosition, distance, fov);
    }
}
