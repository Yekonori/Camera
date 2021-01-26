using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFollowView : AView
{
    public float roll = 0f;
    [Range(0f, 180f)] public float fov = 0f;
    public Transform target;

    private float yaw = 0f;
    private float pitch = 0f;

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        pitch = -Mathf.Asin(target.position.y) * Mathf.Rad2Deg;
    }

    public override CameraConfiguration GetConfiguration()
    {
        return new CameraConfiguration(yaw, pitch, roll, transform.position, 0f, fov);
    }
}
