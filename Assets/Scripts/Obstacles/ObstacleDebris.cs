using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InterpolatedTransform))]
public class ObstacleDebris : ObstacleBase
{
    private InterpolatedTransform interpolatedTransform;

    private void Awake()
    {
        interpolatedTransform = GetComponent<InterpolatedTransform>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        transform.localPosition = new Vector3(position, 0, (float)progress * 1000);
        interpolatedTransform.LateFixedUpdate();
    }
}
