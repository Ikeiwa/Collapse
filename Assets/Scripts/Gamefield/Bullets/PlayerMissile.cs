using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : AbstractPlayerShot
{

    private InterpolatedTransform interpolatedTransform;

    private float currentSpeed = 0.0f;

    void Awake()
    {
        interpolatedTransform = GetComponent<InterpolatedTransform>();
    }

    void FixedUpdate()
    {
        currentSpeed += 0.12f;
        transform.Translate(Vector3.forward * currentSpeed);
        if (gf.IsOOB(transform))
            Kill();

        interpolatedTransform.LateFixedUpdate();
    }

}