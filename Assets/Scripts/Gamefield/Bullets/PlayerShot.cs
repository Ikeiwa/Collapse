using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    private Gamefield gf = Gamefield.instance;
    private InterpolatedTransform interpolatedTransform;

    void Awake()
    {
        interpolatedTransform = GetComponent<InterpolatedTransform>();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * 4.5f);
        if (gf.IsOOB(transform))
            Destroy(gameObject);

        interpolatedTransform.LateFixedUpdate();
    }
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Player shot collision : " + collision.gameObject.tag);
    }
}
