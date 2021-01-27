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
        Init();
    }

    void SetActive(bool isActive)
    {
        //SetActive(isActive);
        if (isActive)
        {
            CameraController.Instance.AddView(this);
        }
    }

    public virtual void OnDrawGizmos()
    {
        CameraConfiguration test = GetConfiguration();
        if (test != null && isActiveOnStart)
        {
            test.DrawGizmos(Color.white);
        }
    }

    protected void Init()
    {
        SetActive(isActiveOnStart);
    }
}
