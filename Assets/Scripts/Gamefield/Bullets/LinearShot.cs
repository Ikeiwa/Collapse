using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearShot : MonoBehaviour
{

    private Gamefield gf = Gamefield.instance;

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * 0.6f);
        if (gf.IsOOB(transform))
            Destroy(gameObject);
    }
}
