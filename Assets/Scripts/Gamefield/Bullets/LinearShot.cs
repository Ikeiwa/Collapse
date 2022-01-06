using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearShot : AbstractBullet
{

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * 0.6f);
        if (gf.IsOOB(transform))
            Kill();
    }
    

}
