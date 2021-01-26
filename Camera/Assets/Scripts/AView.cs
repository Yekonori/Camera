using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    [Min(0f)] public float weight;
    public bool isActiveOnStart = false;
    public abstract CameraConfiguration GetConfiguration();

    protected void Start()
    {
        SetActive(isActiveOnStart);
    }

    void SetActive(bool isActive)
    {
        //SetActive(isActive);
        if (isActive)
        {
            CameraController.Instance.AddView(this);
        }
    }

    public void OnDrawGizmos()
    {
        CameraConfiguration test = GetConfiguration();
        if (test != null && isActiveOnStart)
        {
            test.DrawGizmos(Color.white);
        }
    }
}
