using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    [Min(0f)] public float weight;

    public abstract CameraConfiguration GetConfiguration();

    public void SetActive(bool isActive)
    {
        //SetActive(isActive);
        if (isActive)
        {
            CameraController.Instance.AddView(this);
        }
        else
        {
            CameraController.Instance.RemoveView(this);
        }
    }

    public virtual void OnDrawGizmos()
    {
        CameraConfiguration test = GetConfiguration();
        if (test != null)
        {
            test.DrawGizmos(Color.white);
        }
    }
}
