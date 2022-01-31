using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : AbstractPlayerShot
{
    
    private InterpolatedTransform interpolatedTransform;

    void Awake()
    {
        interpolatedTransform = GetComponent<InterpolatedTransform>();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * 4.5f);
        if (gf.IsOOB(transform))
            Kill();

        interpolatedTransform.LateFixedUpdate();
    }
    
}
