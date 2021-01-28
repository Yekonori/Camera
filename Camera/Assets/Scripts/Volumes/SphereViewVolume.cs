using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereViewVolume : AViewVolume
{
    public Transform target;
    public float outerRadius = 5;
    public float innerRadius = 5;

    private float distance = 0f;

    // Update is called once per frame
    void Update()
    {
        //        Dans la méthode Update, mettre à jour « distance » :
        //• Si « distance <= outerRadius » et « IsActive » vaut faux, appeler « SetActive(true) »
        //• Si « distance > outerRadius » et « IsActive » vaut vrai, appeler « SetActive(false) »

        distance = (target.position - transform.position).magnitude;
        Debug.Log(distance);
        if (distance <= outerRadius && !isActive)
        {
            SetActive(true);
        }
        if (distance > outerRadius && isActive)
        {
            SetActive(false);
        }
    }

    public override float ComputeSelfWeight()
    {
        if (distance <= innerRadius)
            return 1f;

        if (distance > outerRadius)
            return 0f;

        return 1f - (distance - innerRadius) / (outerRadius - innerRadius);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, innerRadius);
        Gizmos.DrawSphere(transform.position, outerRadius);
    }
}
