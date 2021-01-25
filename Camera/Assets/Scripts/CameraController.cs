using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float movementDuration = 5f;
    public List<CameraConfiguration> listConfig;
    private Camera myCam;

    //CameraConfiguration startConfig;
    //CameraConfiguration endConfig;

    private void Start()
    {
        myCam = Camera.main;
        //StartCoroutine(ChangeConfig(startConfig, endConfig));
        StartCoroutine(MoveConfig(listConfig));
    }


    IEnumerator MoveConfig(/*float timer, */List<CameraConfiguration> lConfig)
    {
        if (lConfig.Count == 0)
        {
            yield return null;
        }
        else if (lConfig.Count == 1)
        {
            Quaternion orientation = Quaternion.Euler(lConfig[0].pitch, lConfig[0].yaw, lConfig[0].roll);
            myCam.transform.rotation = orientation;
            Vector3 offset = orientation * (Vector3.back * lConfig[0].distance);
            transform.position = lConfig[0].pivot + offset;
            myCam.fieldOfView = lConfig[0].fieldOfView;
        }
        else if (lConfig.Count == 2)
        {
            float timer = 0f;
            while (timer < movementDuration)
            {
                timer += Time.deltaTime;

                Quaternion startRot = Quaternion.Euler(lConfig[0].pitch, lConfig[0].yaw, lConfig[0].roll);
                Quaternion endRot = Quaternion.Euler(lConfig[1].pitch, lConfig[1].yaw, lConfig[1].roll);
                Quaternion orientation = Quaternion.Lerp(startRot, endRot, timer / movementDuration);
                myCam.transform.rotation = orientation;

                Vector3 offsetStart = startRot * (Vector3.back * lConfig[0].distance);
                Vector3 offsetEnd = endRot * (Vector3.back * lConfig[1].distance);
                Vector3 offset = Vector3.Lerp(offsetStart, offsetEnd, timer / movementDuration);

                myCam.transform.position = Vector3.Lerp(lConfig[0].pivot, lConfig[1].pivot, timer / movementDuration) + offset;
                myCam.fieldOfView = Mathf.Lerp(lConfig[0].fieldOfView, lConfig[1].fieldOfView, timer / movementDuration);

                yield return null;
            }
        }
    }

    //void LerpConfig(float timer, List<CameraConfiguration> lConfig)
    //{
    //    if (lConfig.Count == 2)
    //    {

    //    }
    //    else LerpConfig
    //}
}
