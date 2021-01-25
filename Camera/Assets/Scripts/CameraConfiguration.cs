using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraConfiguration
{
    [Range(0f, 360f)] public float yaw = 0f;
    [Range(-90f, 90f)] public float pitch = 0f;
    [Range(-180, 180)] public float roll = 0f;
    public Vector3 pivot = Vector3.zero;
    [Min(0f)] public float distance = 2f;
    [Range(0f, 180f)] public float fieldOfView = 60f;

    public CameraConfiguration(float _yaw, float _pitch, float _roll, Vector3 _pivot, float _distance, float _fieldOfView)
    {
        yaw = _yaw;
        pitch = _pitch;
        roll = _roll;
        pivot = _pivot;
        distance = _distance;
        fieldOfView = _fieldOfView;
    }
}
