using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldEntity : MonoBehaviour
{

    public Gamefield parent;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        // TODO : remove this, abstract Field entities have no AI
        gameObject.transform.position = gameObject.transform.position + (Vector3.right * 0.1f);
    }

    public bool IntersectsPlayer() {
        return false;
        // TODO: compute player intersection for everything.
    }
}
