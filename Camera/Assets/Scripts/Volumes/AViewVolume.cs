﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AViewVolume : MonoBehaviour
{
    public int priority = 0;
    public AView view;

    protected bool isActive { get; private set; }


    public virtual float ComputeSelfWeight()
    {
        return 1f;
    }

    protected void SetActive(bool _isActive)
    {
        isActive = _isActive;
        if (_isActive)
        {
            ViewVolumeBlender.instance.AddVolume(this);
        }
        else
        {
            ViewVolumeBlender.instance.RemoveVolume(this);
        }
    }
}
