using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallingShot : AbstractBullet
{

    private float speedcurrent = 0.3f;
    private bool stopped = false, despawner = false;
    private int stopframes = 0;

    void FixedUpdate()
    {
        if (!stopped)
        {
            speedcurrent += 0.02f;
            transform.Translate(Vector3.forward * speedcurrent);
        }

        if (transform.position.z <= 0 && !stopped)
        {
            stopped = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            transform.rotation = QUATERNION_DOWN;
            speedcurrent = 0f;
        }
        

        if (stopped && !despawner) {
            stopframes++;
            if (stopframes >= 50) 
                despawner = true;
        }

        if (despawner) {
            speedcurrent += 0.001f;
            transform.Translate(Vector3.forward * speedcurrent);
        }

        if (gf.IsOOB(transform))
            Kill();
    }

}
