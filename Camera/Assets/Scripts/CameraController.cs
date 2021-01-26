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

    public static CameraController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        myCam = Camera.main;
        StartCoroutine(MoveConfig(listConfig));
    }


    IEnumerator MoveConfig(List<CameraConfiguration> lConfig)
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
        else
        {
            float timer = 0f;
            while (timer < movementDuration)
            {
                timer += Time.deltaTime;

                CameraConfiguration bla = CameraConfiguration.ListInterpolation(timer / movementDuration, lConfig);

                Quaternion orientation = Quaternion.Euler(bla.pitch, bla.yaw, bla.roll);
                myCam.transform.rotation = orientation;

                Vector3 offset = orientation * Vector3.back * bla.distance;
                myCam.transform.position = bla.pivot + offset;
                myCam.fieldOfView = bla.fieldOfView;

                yield return null;
            }
        }
    }
}
