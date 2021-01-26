using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float movementDuration = 5f;
    public List<CameraConfiguration> listConfig;
    private Camera myCam;

    private List<AView> activeViews = new List<AView>();

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
        //StartCoroutine(MoveConfig(listConfig));
    }

    private void Update()
    {
        CameraConfiguration interpol = InterpolateFixedView(activeViews);

        Quaternion orientation = Quaternion.Euler(interpol.pitch, interpol.yaw, interpol.roll);
        myCam.transform.rotation = orientation;
        Vector3 offset = orientation * (Vector3.back * interpol.distance);
        myCam.transform.position = interpol.pivot + offset;
        myCam.fieldOfView = interpol.fieldOfView;
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

    public void AddView(AView view)
    {
        activeViews.Add(view);
    }

    public void RemoveView(AView view)
    {
        activeViews.Remove(view);
    }

    CameraConfiguration InterpolateFixedView(List<AView> activeViews)
    {
        if (activeViews.Count == 0)
        {
            Debug.LogError("Ajoute des vues !");
            return new CameraConfiguration(0f, 0f, 0f, Vector3.zero, 5f, 60f);
        }


        float yaw = 0f;
        float pitch = 0f;
        float roll = 0f;
        Vector3 pivot = Vector3.zero;
        float distance = 0f;
        float fieldOfView = 0f;

        float sumWeight = 0f;

        foreach (AView view in activeViews)
        {
            yaw += view.weight * view.GetConfiguration().yaw;
            pitch += view.weight * view.GetConfiguration().pitch;
            roll += view.weight * view.GetConfiguration().roll;
            pivot += view.weight * view.GetConfiguration().pivot;
            distance += view.weight * view.GetConfiguration().distance;
            fieldOfView += view.weight * view.GetConfiguration().fieldOfView;

            sumWeight += view.weight;
        }
        if (sumWeight == 0)
        {
            Debug.LogError("Met des poids à tes vues");
            float nbView = activeViews.Count;
            return new CameraConfiguration(yaw/ nbView, pitch / nbView, roll / nbView, pivot / nbView, distance / nbView, fieldOfView/ nbView);
        }
        yaw /= sumWeight;
        pitch /= sumWeight;
        roll /= sumWeight;
        pivot /= sumWeight;
        distance /= sumWeight;
        fieldOfView /= sumWeight;

        return new CameraConfiguration(yaw, pitch, roll, pivot, distance, fieldOfView);
    }
}
