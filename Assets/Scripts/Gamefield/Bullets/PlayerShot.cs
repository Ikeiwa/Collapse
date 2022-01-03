using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    private Gamefield gf = Gamefield.instance;

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * 4.5f);
        if (gf.IsOOB(transform))
            Destroy(gameObject);
    }
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Player shot collision : " + collision.gameObject.tag);
    }
}
