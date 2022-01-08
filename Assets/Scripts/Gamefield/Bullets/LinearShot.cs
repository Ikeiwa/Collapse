using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearShot : AbstractBullet
{

    private float speed = 0.6f;

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed);
        if (gf.IsOOB(transform))
            Kill();
    }

    public override void OnSpawn(BulletArguments args)
    {
        if(args != BulletArguments.NONE)
            speed = args.speed;
    }

}
